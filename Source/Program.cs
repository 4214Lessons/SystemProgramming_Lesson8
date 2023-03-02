using System.Diagnostics;

var students = StudentHelpers.Generate(20000);


// Parallel
// PLINQ





/*

    Stopwatch sw = new Stopwatch();
    
    sw.Start();
    
    
    var t1 = Task.Run(() =>
    {
        for (int i = 0; i < students.Count / 2; i++)
        {
            students[i].Group = "NEW_GROUP";
            Thread.Sleep(10);
        }
    });
    
    
    var t2 = Task.Run(() =>
    {
        for (int i = students.Count / 2; i < students.Count; i++)
        {
            students[i].Group = "NEW_GROUP";
            Thread.Sleep(10);
        }
    });
    
    
    await Task.WhenAll(t1, t2);
    
    
    
    sw.Stop();
    Console.WriteLine(sw.ElapsedTicks);


// 31228720
// 15567831
// 15596867
// 3982968
// 33863855
// 6287571
*/






/*

    Stopwatch sw = new Stopwatch();
    
    sw.Start();
    
    
    Parallel.ForEach(students, student =>
    {
        student.Group = "NEW_GROUP";
        Thread.Sleep(10);
    });
    
    
    sw.Stop();
    Console.WriteLine(sw.ElapsedTicks);

*/








/*

    Stopwatch sw = new Stopwatch();
    
    sw.Start();
    
    var options = new ParallelOptions()
    {
        // CancellationToken = CancellationToken.None,
        // TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
        MaxDegreeOfParallelism = 5,
    };
    
    
    Parallel.ForEach(students, options, student =>
    {
        student.Group = "NEW_GROUP";
        Thread.Sleep(10);
    });
    
    
    sw.Stop();
    Console.WriteLine(sw.ElapsedTicks);

*/








/*

    Parallel.For(0, students.Count, i =>
    {
        students[i].Group = "NEW_GROUP";
        Thread.Sleep(10);
    });

*/







/*
    Parallel.Invoke(
        () => { }, 
        () => { }, 
        () => { }, 
        () => { }, 
        () => { }, 
        () => { });
*/










/*

Stopwatch sw = Stopwatch.StartNew();

var linqCount = students.Count(s => s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"));

Console.WriteLine($"LINQ ->  Count: {linqCount} -  Ticks: {sw.ElapsedTicks}");


sw.Restart();

var _lock = new object();
var parallelCount = 0;
Parallel.ForEach(students, s =>
{
    if (s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"))
    {
        // Interlocked.Increment(ref parallelCount);
        lock (_lock)
        {
            parallelCount++;
        }
    }
});

Console.WriteLine($"Parallel ->  Count: {parallelCount} -  Ticks: {sw.ElapsedTicks}");

sw.Restart();

// PLINQ
var plinqCount = students.AsParallel().Count(s => s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"));

Console.WriteLine($"PLINQ ->  Count: {plinqCount} -  Ticks: {sw.ElapsedTicks}");

*/








Stopwatch sw = Stopwatch.StartNew();

var namesPLinq = students
    .AsParallel()
    .Where(s => s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"))
    .Select(s => $"{s.FirstName} {s.LastName}")
    .ToList();

Console.WriteLine($"PLINQ count: {namesPLinq.Count}  -  Ticks: {sw.ElapsedTicks}");
sw.Restart();


var namesLinq = students
    .Where(s => s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"))
    .Select(s => $"{s.FirstName} {s.LastName}")
    .ToList();


Console.WriteLine($"LINQ count: {namesLinq.Count}  -  Ticks: {sw.ElapsedTicks}");
sw.Restart();


var namesParallel = new List<string>();
var _lock = new object();

Parallel.ForEach(students, s =>
{
    if (s.FirstName.Length + s.LastName.Length > 15 && s.Email.ToLower().EndsWith("@gmail.com"))
    {
        lock (_lock)
        {
            namesParallel.Add($"{s.FirstName} {s.LastName}");
        }
    }
});


Console.WriteLine($"Parallel count: {namesParallel.Count}  -  Ticks: {sw.ElapsedTicks}");









/*
    var sw = Stopwatch.StartNew();
    
    
    "abc".AsParallel().Count();
    Console.WriteLine(sw.ElapsedTicks);
    
    sw.Restart();
    
    "abc".Count();
    Console.WriteLine(sw.ElapsedTicks);
*/












// Thread-Safe collections