/*
BSD License

Copyright (c) 2013, Kazunori Sakamoto
Copyright (c) 2016, Alexander Alexeev
Copyright (c) 2023, DasDarki for qLua
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
3. Neither the NAME of Rainer Schuster nor the NAMEs of its contributors
   may be used to endorse or promote products derived from this software
   without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

This grammar file derived from:

    Luau 0.537 Grammar Documentation
    https://github.com/Roblox/luau/blob/0.537/docs/_pages/grammar.md

    Lua 5.4 Reference Manual
    http://www.lua.org/manual/5.4/manual.html

    Lua 5.3 Reference Manual
    http://www.lua.org/manual/5.3/manual.html

    Lua 5.2 Reference Manual
    http://www.lua.org/manual/5.2/manual.html

    Lua 5.1 grammar written by Nicolai Mainiero
    http://www.antlr3.org/grammar/1178608849736/Lua.g

Tested by Kazunori Sakamoto with Test suite for Lua 5.2 (http://www.lua.org/tests/5.2/)

Tested by Alexander Alexeev with Test suite for Lua 5.3 http://www.lua.org/tests/lua-5.3.2-tests.tar.gz

Tested by Matt Hargett with:
    - Test suite for Lua 5.4.4: http://www.lua.org/tests/lua-5.4.4-tests.tar.gz
    - Test suite for Selene Lua lint tool v0.20.0: https://github.com/Kampfkarren/selene/tree/0.20.0/selene-lib/tests
    - Test suite for full-moon Lua parsing library v0.15.1: https://github.com/Kampfkarren/full-moon/tree/main/full-moon/tests
    - Test suite for IntelliJ-Luanalysis IDE plug-in v1.3.0: https://github.com/Benjamin-Dobell/IntelliJ-Luanalysis/tree/v1.3.0/src/test
    - Test suite for StyLua formatting tool v.14.1: https://github.com/JohnnyMorganz/StyLua/tree/v0.14.1/tests
    - Entire codebase for luvit: https://github.com/luvit/luvit/
    - Entire codebase for lit: https://github.com/luvit/lit/
    - Entire codebase and test suite for neovim v0.7.2: https://github.com/neovim/neovim/tree/v0.7.2
    - Entire codebase for World of Warcraft Interface: https://github.com/tomrus88/BlizzardInterfaceCode
    - Benchmarks and conformance test suite for Luau 0.537: https://github.com/Roblox/luau/tree/0.537
*/

grammar QLua;

chunk
    : block EOF
    ;

block
    : stat* declaration* laststat?
    | declaration* stat* laststat?
    ;

stat
    : stat_semicolon
    | stat_varassign
    | functioncall
    | label
    | stat_break
    | stat_continue
    | stat_goto
    | stat_do
    | stat_while
    | stat_repeat
    | stat_if
    | stat_for
    | stat_forin
// qLua specific
    | classLevelStat
    | constructorCall
// end qLua specific
    ;

stat_semicolon : ';';
stat_varassign : varlist '=' explist;
stat_break : 'break' INT?;
stat_continue : 'continue' INT?;
stat_goto : 'goto' NAME;
stat_do : 'do' block 'end';
stat_while : 'while' exp 'do' block 'end';
stat_repeat : 'repeat' block 'until' exp;
stat_if : stat_if_branch stat_elseif_branch* stat_else_branch? 'end';
stat_if_branch : 'if' exp 'then' block ;
stat_elseif_branch : 'elseif' exp 'then' block ;
stat_else_branch : 'else' block ;
stat_for : 'for' NAME '=' exp ',' exp (',' exp)? 'do' block 'end';
stat_forin : 'for' namelist 'in' explist 'do' block 'end';
    
classLevelStat
    : clstat_func1
    | clstat_func2
    | clstat_vardecl
// qLua specific
    | clstat_tabledecon
    | clstat_classdef
    | clstat_interfacedef
    | clstat_enumdef
// end qLua specific
    ;

clstat_func1 : 'function' funcname funcbody ;
clstat_func2 : 'local' 'function' NAME funcbody ;
clstat_vardecl : 'local'? attnamelist ('=' explist)? ;
clstat_tabledecon : 'local'? tableDeconstructionList '=' explist ;
clstat_classdef : 'local'? classDefinition ;
clstat_interfacedef : 'local'? interfaceDefinition ;
clstat_enumdef : 'local'? enumDefinition ;

attnamelist
    : attname (',' attname)*
    ;
    
attname
    : NAME typeAnnotation? attrib?
    | NAME attrib? typeAnnotation?
    ;

attrib
    : '<' NAME '>'
    ;

laststat
    : ('return' explist? | 'break' INT? | 'continue' INT?) ';'?
    ;

label
    : '::' NAME '::'
    ;

funcname
    : NAME ('.' NAME)* (':' NAME)?
    ;

varlist
    : var (',' var)*
    ;

namelist
    : NAME (',' NAME)*
    ;

explist
    : (exp ',')* exp
    ;

exp
    : 'nil' | 'false' | 'true'
    | number
    | string
    | '...'
    | functiondef
    | prefixexp
// qLua specific
    | exp 'as' nilableType
    | exp '?' exp ':' exp
// end qLua specific
    | tableconstructor
    | <assoc=right> exp operatorPower exp
    | operatorUnary exp
    | exp operatorMulDivMod exp
    | exp operatorAddSub exp
    | <assoc=right> exp operatorStrcat exp
    | exp operatorComparison exp
    | exp operatorAnd exp
    | exp operatorOr exp
    | exp operatorBitwise exp
    ;

prefixexp
    : varOrExp nameAndArgs*
    ;

functioncall
    : varOrExp nameAndArgs+
    ;

varOrExp
    : var | '(' exp ')'
    ;

var
    : (NAME | '(' exp ')' varSuffix) varSuffix*
    ;

varSuffix
    : nameAndArgs* ('[' exp ']' | ('?')? '.' NAME)
    ;

nameAndArgs
    : (('?')? ':' NAME)? args
    ;

args
    : '(' explist? ')' | tableconstructor | string
    ;

functiondef
    : 'function' funcbody
    ;

funcbody
    : funchead block 'end'
    ;

funchead
    : '(' parlist? ')' typeAnnotation?
    ;
    
parlist
    : param (',' param)* (',' paramVararg)? | paramVararg
    ;
    
param
    : NAME typeAnnotation?
    ;
    
paramVararg
    : '...' typeAnnotation?
    ;

tableconstructor
    : '{' fieldlist? '}'
    ;

fieldlist
    : field (fieldsep field)* fieldsep?
    ;

field
    : '[' exp ']' '=' exp | NAME '=' exp | exp
    ;

fieldsep
    : ',' | ';'
    ;

operatorOr
	: 'or' | '||';

operatorAnd
	: 'and' | '&&';

operatorComparison
	: '<' | '>' | '<=' | '>=' | '~=' | '==' | '!=' | '===' | '!==' | 'instanceof' | 'is' | '??';

operatorStrcat
	: '..' | '+';

operatorAddSub
	: '+' | '-';

operatorMulDivMod
	: '*' | '/' | '%' | '//';

operatorBitwise
	: '&' | '|' | '~' | '<<' | '>>';

operatorUnary
    : 'not' | '#' | '-' | '~' | '!' | '++' | '--';

operatorPower
    : '^';

number
    : INT | HEX | FLOAT | HEX_FLOAT
    ;

string
    : NORMALSTRING | CHARSTRING | LONGSTRING | TEMPLATESTRING
    ;
    
// qLua specific
enumDefinition
    : 'enum' NAME enumBody 'end'
    ;
    
enumBody
    : enumProperty*
    ;
    
enumProperty
    : NAME
    | NAME '=' enumValue
    ;
    
enumValue
    : 'nil' | 'false' | 'true'
    | number
    | string
    | tableconstructor
    ;

classDefinition
    : 'class' NAME classExtends? classImplements? classBody 'end'
    ;
    
classExtends
    : 'extends' NAME
    ;
    
classImplements
    : 'implements' NAME (',' NAME)*
    ;
    
classBody
    : classLevelStat*
    ;
    
interfaceDefinition
    : 'interface' NAME interfaceExtends? interfaceBody 'end'
    ;
    
interfaceExtends
    : 'extends' NAME (',' NAME)*
    ;
    
interfaceBody
    : interfaceStat*
    ;
    
interfaceStat
    : interfaceFunction
    | interfaceField
    ;
    
interfaceFunction
    : 'function' funcname funchead
    ;
    
interfaceField
    : NAME typeAnnotation
    ;

constructorCall
    : 'new' constructorCallArgs
    ;
    
constructorCallArgs
    : NAME '(' explist? ')'
    ;

typeAnnotation
    : ':' nilableType
    | ':' typeTuple
    ;
    
typeTuple
    : '(' type (',' type)* ')'
    ;


nilableType
    : type '| nil'
    | type '?'
    | '?' type
    | type
    ;

type
    : NAME
    | basicType
    | tableType
    | arrayType
    | funcType
    ;

basicType
    : 'any' | 'boolean' | 'number' | 'string' | 'table' | 'function' | 'userdata' | 'thread'
    ;
    
funcType
    : '(' parlist? ')' typeAnnotation?
    ;
    
tableType
    : '{' (tableField (',' tableField)*)? '}'
    ;
    
tableField
    : '[' exp ']' ':' type
    | NAME ':' type
    | NAME
    ;

arrayType
    : baseTypeForArray '[' ']'
    | '[' type (',' type)* ']'
    ;

baseTypeForArray
    : NAME
    | basicType
    | tableType
    ;
    
tableDeconstructionList
    : tableDeconstruction (',' tableDeconstruction)*
    ;
    
tableDeconstruction
    : '{' (tableDeconstructionField (',' tableDeconstructionField)*)? '}'
    ;
    
tableDeconstructionField
    : NAME
    | NAME 'as' NAME
    | '[' exp ']' 'as' NAME
    | tableDeconstruction
    ;
    
// qLua Declarations
declaration
    : moduleDeclaration
    | moduleLevelDeclStat
    ;

moduleDeclaration
    : 'declare' 'module' string moduleDeclBody 'end'
    ;
    
moduleDeclBody
    : moduleLevelDeclStat*
    ;
    
moduleLevelDeclStat
    : 'declare' interfaceFunction
    | 'declare' interfaceField
    | classDeclaration
    | interfaceDeclaration
    | enumDeclaration
    ;
    
classDeclaration
    : 'declare' 'class' NAME classExtends? classImplements? classDeclBody 'end'
    ;
    
classDeclBody
    : moduleLevelDeclStat*
    ;
    
interfaceDeclaration
    : 'declare' 'interface' NAME interfaceExtends? interfaceDeclBody 'end'
    ;
    
interfaceDeclBody
    : interfaceDeclStat*
    ;
    
interfaceDeclStat
    : 'declare' interfaceFunction
    | 'declare' interfaceField
    ;
    
enumDeclaration
    : 'declare' 'enum' NAME enumDeclBody 'end'
    ;
    
enumDeclBody
    : enumProperty*
    ;

// LEXER

NAME
    : [a-zA-Z_][a-zA-Z_0-9]*
    ;

NORMALSTRING
    : '"' ( EscapeSequence | ~('\\'|'"') )* '"'
    ;

CHARSTRING
    : '\'' ( EscapeSequence | ~('\''|'\\') )* '\''
    ;
    
TEMPLATESTRING
    : '`' ( EscapeSequence | ~('`'|'\\') )* '`'
    ;

LONGSTRING
    : '[' NESTED_STR ']'
    ;

fragment
NESTED_STR
    : '=' NESTED_STR '='
    | '[' .*? ']'
    ;

INT
    : Digit+
    ;

HEX
    : '0' [xX] HexDigit+
    ;

FLOAT
    : Digit+ '.' Digit* ExponentPart?
    | '.' Digit+ ExponentPart?
    | Digit+ ExponentPart
    ;

HEX_FLOAT
    : '0' [xX] HexDigit+ '.' HexDigit* HexExponentPart?
    | '0' [xX] '.' HexDigit+ HexExponentPart?
    | '0' [xX] HexDigit+ HexExponentPart
    ;

fragment
ExponentPart
    : [eE] [+-]? Digit+
    ;

fragment
HexExponentPart
    : [pP] [+-]? Digit+
    ;

fragment
EscapeSequence
    : '\\' [abfnrtvz"'|$#\\]   // World of Warcraft Lua additionally escapes |$# 
    | '\\' '\r'? '\n'
    | DecimalEscape
    | HexEscape
    | UtfEscape
    ;

fragment
DecimalEscape
    : '\\' Digit
    | '\\' Digit Digit
    | '\\' [0-2] Digit Digit
    ;

fragment
HexEscape
    : '\\' 'x' HexDigit HexDigit
    ;

fragment
UtfEscape
    : '\\' 'u{' HexDigit+ '}'
    ;

fragment
Digit
    : [0-9]
    ;

fragment
HexDigit
    : [0-9a-fA-F]
    ;

fragment
SingleLineInputCharacter
    : ~[\r\n\u0085\u2028\u2029]
    ;

COMMENT
    : '--[' NESTED_STR ']' -> channel(HIDDEN)
    ;

LINE_COMMENT
    : '--' SingleLineInputCharacter* -> channel(HIDDEN)
    ;

WS
    : [ \t\u000C\r\n]+ -> skip
    ;

SHEBANG
    : '#' '!' SingleLineInputCharacter* -> channel(HIDDEN)
    ;