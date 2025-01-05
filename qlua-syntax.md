## Defining a variable
```
local x: number = 0
local y: string = "hello"
local z: boolean = true
local t: Table = { a = 1, b = 2 }
local f: Function = function() end
local n: nil = nil
local a: any = 1
local m: string | number = 1
```

## Defining a function
```
local function findString(x: number, y: string): string
    return y
end
```

### Multiple return values, no problem
```
local function findString(x: number, y: string): string, number
    return y, x
end
```

## Defining a class
```
class Person
    local name: string
    local age: number
    
    publicAccess: boolean = true

    function Person(name: string, age: number)
        self.name = name
        self.age = age
    end

    function getName(): string
        return self.name
    end

    function getAge(): number
        return self.age
    end
end
```

## Construct an object
```
local p: Person = Person("John", 20)
p.publicAccess = false

print(p.getName())
print(p.getAge())
print(p.publicAccess)
```

## Extending a class
```
class Student extends Person
    local grade: number

    function Student(name: string, age: number, grade: number)
        parent(name, age)
        self.grade = grade
    end

    function getGrade(): number
        return self.grade
    end
    
    function getAge(): number
        return parent.getAge() + 1
    end
    
    function getTransformedName(): string
        return parent.name 
    end
end
```

## Define an interface
```
interface IShape
    function getArea(): number
end

class Circle implements IShape
    local radius: number

    function Circle(radius: number)
        self.radius = radius
    end

    function getArea(): number
        return 3.14 * self.radius * self.radius
    end
end
```

## Define a generic class
```
class Pair<T, U>
    local first: T
    local second: U

    function Pair(first: T, second: U)
        self.first = first
        self.second = second
    end

    function getFirst(): T
        return self.first
    end

    function getSecond(): U
        return self.second
    end
end
```

### Be more specific with generics
```
class NumeralPair<T extends number, U extends number>
    local first: T
    local second: U

    function NumeralPair(first: T, second: U)
        self.first = first
        self.second = second
    end

    function getFirst(): T
        return self.first
    end

    function getSecond(): U
        return self.second
    end
end
```

## Better string concatenation
```
local x: string = "hello"
local y: string = "world"
local z: string = x + y
```

## or with templating?
```
local x: string = "name"
local y: string = "Hello World, ${x}!"
```

## Inclusive boolean logic
```
local luaLike: boolean = x ~= y
local allOtherLike: boolean = x != y
```

## Nullablity to make it safe
```
local x: string | nil = nil
local y: string | nil = "hello"
```

### Be safe when calling a null value
```
local x: string | nil = nil
local y: string | nil = "hello"

local z: string = x or "default"

local p: Person | nil = nil
local q: Person | nil = Person("John", 20)

local r: string = p?.getName() or "default"
```

## Enums for everyone
```
enum Color
    Red,
    Green,
    Blue
end
```

### With values
```
enum Color
    Red = 1,
    Green = 2,
    Blue = 3
end
```

### With complexer values
```
enum Color
    Red = { r = 255, g = 0, b = 0 },
    Green = { r = 0, g = 255, b = 0 },
    Blue = { r = 0, g = 0, b = 255 }
end
```

## Type checking and instanceof
```
local x: number = 0
local y: Person = Person("John", 20)
local z: Student = Student("John", 20, 10)

if x is number then
    print("x is a number")
end

if y is Person then
    print("y is a Person")
end

if z instanceof Person then
    print("z is a Person too!")
end
```

## Type casting
```
local x: number = 0
local y: Person = Person("John", 20)

local z: number = x as number
local w: Person = y as Person
```

## Declare types for external libraries
```
declare require "some/library.lua"
    declare class Module 
        function DoSomething(): void
    end
    
    return Module
end

declare class Vector3
    x: number
    y: number
    z: number

    function Vector3(x: number, y: number, z: number)

    function add(v: Vector3): Vector3
    function sub(v: Vector3): Vector3
    function mul(v: Vector3): Vector3
    function div(v: Vector3): Vector3
end
```

## Require on steroids
```
-- boring require. just loads the file into the global namespace
require "some/library.lua"

-- requires the file and loads it into a local variable named someName
require "some/library.lua" as someName

-- requires the file and deconstructs the module into local variables
require "some/library.lua" as { someName, someOtherName }
```

## Special thing: Annotations
Annotations have the cool functionality to generate code at compile time. This is useful for things like generating a `__tostring` metamethod for a class, or generating a `__call` metamethod for a function.
### Defining an annotation
```
annotation ToString
    function generateCode(target: Class): void
        // NEED TO FIGURE OUT HOW TO DO THIS
    end
end
```

### Using an annotation
```
@ToString
class Person
    local name: string
    local age: number
    
    publicAccess: boolean = true

    function Person(name: string, age: number)
        self.name = name
        self.age = age
    end

    function getName(): string
        return self.name
    end

    function getAge(): number
        return self.age
    end
end
```
### And annotations can have parameters
```
annotation MultipliedVersion(property: Property, factor: number)
    // NEED TO FIGURE OUT HOW TO DO THIS
end

class Vector3
    @MultipliedVersion(2)
    x: number
    @MultipliedVersion(3)
    y: number
    @MultipliedVersion(4)
    z: number
end
```

## one last thing: override operators
```
class Vector3
    x: number
    y: number
    z: number

    operator + (v: Vector3): Vector3
        return Vector3(self.x + v.x, self.y + v.y, self.z + v.z)
    end
    
    operator - (v: Vector3): Vector3
        return Vector3(self.x - v.x, self.y - v.y, self.z - v.z)
    end
end
```