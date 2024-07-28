#pragma warning disable IDE0003 // disable lint for 'this' qualification
using System.Diagnostics;

// ----------------------------------
// E N T R Y P O I N T
// ----------------------------------
var frames = 50;
var particles = 1_000_000;

ProfileSystem("Simple", ()=>new SimpleParticles.ParticleSystem(particles), frames);
ProfileSystem("Simd", ()=>new SimdParticles.ParticleSystem(particles), frames);

// ----------------------------------
// T Y P E   D E F I N I T I O N S
// ----------------------------------

void ProfileSystem(string name, Func<IParticleSystem> factory, int frames) {
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Running ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(name);
    Console.ForegroundColor = ConsoleColor.Gray;

    IParticleSystem p;

    using( new ProfileRegion("Init") ) {
        p = factory();
    }

    using( new ProfileRegion($"Update") ) {
        for( var i = 0; i < frames; ++i ) {
            p.Update(0.1f);
        }
    }
}

struct ProfileRegion : IDisposable {
    string title;
    Stopwatch stopwatch = new Stopwatch();

    public ProfileRegion(string title) {
        this.title = title;
        this.stopwatch.Start();
    }

    public void Dispose()
    {
        this.stopwatch.Stop();
        Console.Write($"    {this.title} - ");
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{this.stopwatch.ElapsedMilliseconds}ms");
        
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}