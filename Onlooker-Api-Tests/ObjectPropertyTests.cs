using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using Onlooker.ObjectProperties;
using Onlooker.ObjectProperties.Animation;

namespace Onlooker_Api_Tests;

public class ObjectPropertyTests
{
    [Test]
    public void TestIntegerProperty()
    {
        var integer = new IntegerProperty(0);

        var animator = integer.Animate(100, new AnimationSettings
        {
            Interval = TimeSpan.FromMilliseconds(5), 
            Length = TimeSpan.FromSeconds(1)
        });
        
        Console.WriteLine("Init");

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        animator.Start(CancellationToken.None).Wait();
        
        stopwatch.Stop();
        Console.WriteLine(stopwatch.Elapsed);
        
        Console.WriteLine("Done");
    }
}