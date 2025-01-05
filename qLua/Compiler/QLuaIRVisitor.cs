using qLua.Compiler.CodeAnalysis;
using qLua.Compiler.Diagnostics;
using qLua.Compiler.IR;

namespace qLua.Compiler;

/// <summary>
/// The qlua IR visitor takes the parsed AST and generates the intermediate representation.
/// </summary>
public sealed class QLuaIRVisitor(DiagnosticsBag diagnostics) : QLuaBaseVisitor<IRNode?>
{
    public override IRScript VisitChunk(QLuaParser.ChunkContext context)
    {
        var block = VisitBlock(context.block());
        if (block is not IRBlock irBlock)
        {
            return new IRScript(new IRBlock(context.Start, context.Stop, new List<IRStatement>()));
        }
        
        return new IRScript(irBlock);
    }

    public override IRNode VisitBlock(QLuaParser.BlockContext context)
    {
        var statements = new List<IRStatement>();
        
        foreach (var stat in context.stat())
        {
            if (VisitStat(stat) is IRStatement statement)
            {
                statements.Add(statement);
            }
            else
            {
                diagnostics.ReportError(
                    stat.Start,
                    stat.Stop,
                    DiagnosticCode.ERR_PRS_000
                );
            }
        }

        return new IRBlock(context.Start, context.Stop, statements);
    }

    public override IRNode? VisitStat(QLuaParser.StatContext context)
    {
        if (context.stat_semicolon() != null)
        {
            return null;
        }

        if (context.stat_varassign() != null)
        {
            var vars = new List<IRVariableAssignment>();
            
            for (var i = 0; i < context.stat_varassign().varlist().var().Length; i++)
            {
                var varName = VisitVar(context.stat_varassign().varlist().var(i)) as IRVariable;
                
                IRExpression? value = null;
                var valueExpNode = context.stat_varassign().explist()?.exp(i);
                if (valueExpNode != null && VisitExp(valueExpNode) is IRExpression valueExp)
                {
                    value = valueExp;
                }
                
                vars.Add(new IRVariableAssignment(
                    context.Start,
                    context.Stop,
                    varName!, // !, only happens on error, we will handle it through diagnostics
                    value
                ));
            }


            if (vars.Count == 1)
            {
                return vars[0];
            }
            
            return new IRVariableAssignmentList(context.Start, context.Stop, vars);
        }

        if (context.label() != null)
        {
            return new IRLabelStatement(context.Start, context.Stop, context.label().NAME().GetText());
        }

        if (context.functioncall() != null)
        {
            //TODO: implement this
        }
        
        if (context.stat_break() != null)
        {
            var depth = 1;
            if (context.stat_break().INT() != null)
            {
                depth = int.Parse(context.stat_break().INT().GetText());
            }
            
            return new IRBreakStatement(context.Start, context.Stop, depth);
        }
        
        if (context.stat_continue() != null)
        {
            var depth = 1;
            if (context.stat_continue().INT() != null)
            {
                depth = int.Parse(context.stat_continue().INT().GetText());
            }
            
            return new IRContinueStatement(context.Start, context.Stop, depth);
        }

        if (context.stat_goto() != null)
        {
            return new IRGotoStatement(context.Start, context.Stop, context.stat_goto().NAME().GetText());
        }

        if (context.stat_do() != null)
        {
            return new IRDoStatement(
                context.Start,
                context.Stop,
                (IRBlock)VisitBlock(context.stat_do().block())
            );
        }
        
        if (context.stat_while() != null)
        {
            return new IRWhileStatement(
                context.Start,
                context.Stop,
                (IRExpression)VisitExp(context.stat_while().exp()),
                (IRBlock)VisitBlock(context.stat_while().block())
            );
        }
        
        if (context.stat_repeat() != null)
        {
            return new IRRepeatStatement(
                context.Start,
                context.Stop,
                (IRExpression)VisitExp(context.stat_repeat().exp()),
                (IRBlock)VisitBlock(context.stat_repeat().block())
            );
        }
        
        if (context.stat_if() != null)
        {
            var ifCondition = (IRExpression)VisitExp(context.stat_if().stat_if_branch().exp());
            var ifBlock = (IRBlock)VisitBlock(context.stat_if().stat_if_branch().block());
            var elseIfs = new Dictionary<IRExpression, IRBlock>();
            
            foreach (var elseIfBranch in context.stat_if().stat_elseif_branch())
            {
                var elseIfCondition = (IRExpression)VisitExp(elseIfBranch.exp());
                var elseIfBlock = (IRBlock)VisitBlock(elseIfBranch.block());
                elseIfs.Add(elseIfCondition, elseIfBlock);
            }
            
            IRBlock? elseBlock = null;
            if (context.stat_if().stat_else_branch() != null)
            {
                elseBlock = (IRBlock)VisitBlock(context.stat_if().stat_else_branch().block());
            }
            
            return new IRIfStatement(
                context.Start,
                context.Stop,
                ifCondition,
                ifBlock,
                elseIfs,
                elseBlock
            );
        }

        if (context.stat_for() != null)
        {
            var variable = new IRSimpleVariable(
                context.stat_for().NAME().Symbol, 
                context.stat_for().NAME().Symbol,
                context.stat_for().NAME().GetText()
            );
            
            var start = (IRExpression)VisitExp(context.stat_for().exp(0));
            var end = (IRExpression)VisitExp(context.stat_for().exp(1));
            
            IRExpression? step = null;
            if (context.stat_for().exp().Length > 2)
            {
                step = (IRExpression)VisitExp(context.stat_for().exp(2));
            }
            
            return new IRForStatement(
                context.Start,
                context.Stop,
                variable,
                start,
                end,
                step,
                (IRBlock)VisitBlock(context.stat_for().block())
            );
        }
        
        if (context.stat_forin() != null)
        {
            var expressions = new List<IRExpression>();
            var variables = context.stat_forin().namelist().NAME()
                .Select(nameNode => new IRSimpleVariable(nameNode.Symbol, nameNode.Symbol, nameNode.GetText()))
                .Cast<IRVariable>().ToList();

            foreach (var expNode in context.stat_forin().explist().exp())
            {
                if (VisitExp(expNode) is IRExpression expression)
                {
                    expressions.Add(expression);
                }
                else
                {
                    diagnostics.ReportError(
                        expNode.Start,
                        expNode.Stop,
                        DiagnosticCode.ERR_PRS_006
                    );
                }
            }
            
            if (variables.Count != expressions.Count)
            {
                diagnostics.ReportError(
                    context.Start,
                    context.Stop,
                    DiagnosticCode.ERR_PRS_007,
                    variables.Count, 
                    expressions.Count
                );
            }
            
            return new IRForInStatement(
                context.Start,
                context.Stop,
                variables,
                expressions,
                (IRBlock)VisitBlock(context.stat_forin().block())
            );
        }

        if (context.classLevelStat() != null)
        {
            return VisitClassLevelStat(context.classLevelStat());
        }
        
        return null;
    }

    public override IRNode? VisitClassLevelStat(QLuaParser.ClassLevelStatContext context)
    {
        if (context.clstat_func1() != null)
        {
            
        }

        if (context.clstat_func2() != null)
        {
            
        }

        if (context.clstat_vardecl() != null)
        {
            var isLocal = context.clstat_vardecl().GetText().StartsWith("local");
            var vars = new List<IRVariableDeclaration>();

            for (var i = 0; i < context.clstat_vardecl().attnamelist().attname().Length; i++)
            {
                var varName = (IRVariableName)VisitAttname(context.clstat_vardecl().attnamelist().attname(i));
                IRExpression? value = null;

                if (context.clstat_vardecl().explist() != null)
                {
                    if (VisitExp(context.clstat_vardecl().explist().exp(i)) is IRExpression valueExpr)
                    {
                        value = valueExpr;
                    }
                    else
                    {
                        diagnostics.ReportError(
                            context.clstat_vardecl().explist().exp(i).Start,
                            context.clstat_vardecl().explist().exp(i).Stop,
                            DiagnosticCode.ERR_PRS_000
                        );
                    }
                }
                
                vars.Add(new IRVariableDeclaration(
                    context.Start,
                    context.Stop,
                    isLocal,
                    varName,
                    value
                ));
            }

            if (vars.Count == 1)
            {
                return vars[0];
            }
            
            return new IRVariableDeclarationList(context.Start, context.Stop, vars);
        }

        if (context.clstat_tabledecon() != null)
        {
            
        }

        if (context.clstat_classdef() != null)
        {
            
        }

        if (context.clstat_interfacedef() != null)
        {
            
        }

        if (context.clstat_enumdef() != null)
        {
            
        }
        
        return null;
    }

    public override IRNode VisitExp(QLuaParser.ExpContext context)
    {
        var text = context.GetText();
        
        if (text == "nil")
        {
            return new IRNilLiteral(context.Start, context.Stop);
        }
        
        if (text == "false")
        {
            return new IRBooleanLiteral(context.Start, context.Stop, false);
        }
        
        if (text == "true")
        {
            return new IRBooleanLiteral(context.Start, context.Stop, true);
        }

        if (text == "...")
        {
            return new IRSimpleVariable(context.Start, context.Stop, "...");
        }

        if (context.number() != null)
        {
            var numberText = context.number().GetText();
            var isHex = numberText.StartsWith("0x") || numberText.StartsWith("0X");
            var value = isHex ? double.Parse(numberText[2..], System.Globalization.NumberStyles.HexNumber) : double.Parse(numberText);
            
            return new IRNumberLiteral(context.Start, context.Stop, value, isHex);
        }

        if (context.@string() != null)
        {
            var textValue = context.@string().GetText();
            var startDelimiter = "";
            var endDelimiter = "";
            
            if (textValue.StartsWith("\""))
            {
                textValue = textValue[1..^1];
                startDelimiter = "\"";
                endDelimiter = "\"";
            }
            else if (textValue.StartsWith("'"))
            {
                textValue = textValue[1..^1];
                startDelimiter = "'";
                endDelimiter = "'";
            }
            else if (textValue.StartsWith("`"))
            {
                textValue = textValue[1..^1];
                startDelimiter = "`";
                endDelimiter = "`";
            }
            else if (textValue.StartsWith("["))
            {
                var nestedCount = 0;
                foreach (var t in textValue)
                {
                    if (t == '[')
                    {
                        nestedCount++;
                        startDelimiter += "[";
                        endDelimiter = "]" + endDelimiter;
                    }
                    else if (t == '=')
                    {
                        nestedCount++;
                        startDelimiter += "=";
                        endDelimiter = "=" + endDelimiter;
                    }
                    else
                    {
                        break;
                    }
                }
                
                textValue = textValue[nestedCount..^nestedCount];
            }
            
            return new IRStringLiteral(context.Start, context.Stop, startDelimiter, endDelimiter, textValue);
        }
        
        //TODO: write this method

        return null!;
    }

    public override IRNode? VisitVar(QLuaParser.VarContext context)
    {
         IRVariable baseVariable;

        if (context.NAME() != null)
        {
            baseVariable = new IRSimpleVariable(
                context.Start,
                context.Stop,
                context.NAME().GetText()
            );
        }
        else if (context.exp() != null)
        {
            if (VisitExp(context.exp()) is not IRExpression baseExpression)
            {
                diagnostics.ReportError(
                    context.exp().Start,
                    context.exp().Stop,
                    DiagnosticCode.ERR_PRS_001
                );
                return null;
            }

            if (baseExpression is not IRVariable variable)
            {
                diagnostics.ReportError(
                    context.Start,
                    context.Stop,
                    DiagnosticCode.ERR_PRS_002
                );
                return null;
            }
            
            baseVariable = variable;
        }
        else
        {
            diagnostics.ReportError(
                context.Start,
                context.Stop,
                DiagnosticCode.ERR_PRS_003
            );
            return null;
        }

        foreach (var suffix in context.varSuffix())
        {
            if (suffix.NAME() != null)
            {
                baseVariable = new IRFieldAccessVariable(
                    suffix.Start,
                    suffix.Stop,
                    baseVariable,
                    suffix.NAME().GetText()
                );
            }
            else if (suffix.exp() != null)
            {
                if (VisitExp(suffix.exp()) is not IRExpression indexExpression)
                {
                    diagnostics.ReportError(
                        suffix.exp().Start,
                        suffix.exp().Stop,
                        DiagnosticCode.ERR_PRS_004
                    );
                    continue;
                }

                baseVariable = new IRArrayAccessVariable(
                    suffix.Start,
                    suffix.Stop,
                    baseVariable,
                    indexExpression
                );
            }

            foreach (var nameAndArgs in suffix.nameAndArgs())
            {
                var arguments = new List<IRExpression>();

                if (nameAndArgs.args().explist() != null)
                {
                    foreach (var exp in nameAndArgs.args().explist().exp())
                    {
                        if (VisitExp(exp) is not IRExpression argumentExpression)
                        {
                            diagnostics.ReportError(
                                exp.Start,
                                exp.Stop,
                                DiagnosticCode.ERR_PRS_005
                            );
                            continue;
                        }

                        arguments.Add(argumentExpression);
                    }
                }

                baseVariable = new IRFunctionCall(
                    nameAndArgs.Start,
                    nameAndArgs.Stop,
                    baseVariable,
                    arguments
                );
            }
        }

        return baseVariable;
    }

    public override IRNode VisitAttname(QLuaParser.AttnameContext context)
    {
        var typeAnnotation = context.typeAnnotation() != null
            ? (IRTypeAnnotation)VisitTypeAnnotation(context.typeAnnotation())
            : null;
        
        var attribute = context.attrib() != null 
            ? context.attrib().NAME().GetText() 
            : null;
        
        return new IRVariableName(
            context.Start,
            context.Stop,
            context.NAME().GetText(),
            typeAnnotation,
            attribute
        );
    }

    public override IRNode VisitTypeAnnotation(QLuaParser.TypeAnnotationContext context)
    {
        return new IRTypeAnnotation(context.Start, context.Stop);
    }
}