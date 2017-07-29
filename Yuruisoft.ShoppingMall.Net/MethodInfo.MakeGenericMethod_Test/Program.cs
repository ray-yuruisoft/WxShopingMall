using System;
using System.Reflection;



//下面的代码示例演示的属性和方法 MethodInfo 支持泛型方法的检查。 该示例将执行以下操作︰
//定义有一个泛型方法的类。
//创建 MethodInfo 表示泛型方法。
//显示泛型方法定义的属性。
//分配类型的类型参数的变量 MethodInfo, ，并调用结果的构造泛型方法。
//显示构造的泛型方法的属性。
//从构造方法检索泛型方法定义，并将其与原始定义进行比较。
// Define a class with a generic method.
public class Example
{
    public static void Generic<T>(T toDisplay)
    {
        Console.WriteLine("\r\nHere it is: {0}", toDisplay);
    }
}

public class Test
{
    public static void Main()
    {
        Console.WriteLine("\r\n--- Examine a generic method.");

        // Create a Type object representing class Example, and
        // get a MethodInfo representing the generic method.
        //
        Type ex = typeof(Example);
        
        MethodInfo mi = ex.GetMethod("Generic");

        DisplayGenericMethodInfo(mi);

        // Assign the int type to the type parameter of the Example 
        // method.
        //
        MethodInfo miConstructed = mi.MakeGenericMethod(typeof(int));

        DisplayGenericMethodInfo(miConstructed);

        // Invoke the method.
        object[] args = { 42 };
        miConstructed.Invoke(null, args);

        // Invoke the method normally.
        Example.Generic<int>(42);

        // Get the generic type definition from the closed method,
        // and show it's the same as the original definition.
        //
        MethodInfo miDef = miConstructed.GetGenericMethodDefinition();
        Console.WriteLine("\r\nThe definition is the same: {0}",
            miDef == mi);


        Console.ReadKey();
    }

    private static void DisplayGenericMethodInfo(MethodInfo mi)
    {
        Console.WriteLine("\r\n{0}", mi);

        Console.WriteLine("\tIs this a generic method definition? {0}",
            mi.IsGenericMethodDefinition);

        Console.WriteLine("\tIs it a generic method? {0}",
            mi.IsGenericMethod);

        Console.WriteLine("\tDoes it have unassigned generic parameters? {0}",
            mi.ContainsGenericParameters);

        // If this is a generic method, display its type arguments.
        //
        if (mi.IsGenericMethod)
        {
            Type[] typeArguments = mi.GetGenericArguments();

            Console.WriteLine("\tList type arguments ({0}):",
                typeArguments.Length);

            foreach (Type tParam in typeArguments)
            {
                // IsGenericParameter is true only for generic type
                // parameters.
                //
                if (tParam.IsGenericParameter)
                {
                    Console.WriteLine("\t\t{0}  parameter position {1}" +
                        "\n\t\t   declaring method: {2}",
                        tParam,
                        tParam.GenericParameterPosition,
                        tParam.DeclaringMethod);
                }
                else
                {
                    Console.WriteLine("\t\t{0}", tParam);
                }
            }
        }
    }
}

/* This example produces the following output:

--- Examine a generic method.

Void Generic[T](T)
        Is this a generic method definition? True
        Is it a generic method? True
        Does it have unassigned generic parameters? True
        List type arguments (1):
                T  parameter position 0
                   declaring method: Void Generic[T](T)

Void Generic[Int32](Int32)
        Is this a generic method definition? False
        Is it a generic method? True
        Does it have unassigned generic parameters? False
        List type arguments (1):
                System.Int32

Here it is: 42

Here it is: 42

The definition is the same: True

 */