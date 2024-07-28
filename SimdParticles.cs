#pragma warning disable IDE0003 // disable lint for 'this' qualification
using System.Numerics;
using System.Runtime.Intrinsics;

namespace SimdParticles {
    class ParticleSystem : IParticleSystem {
        private Vector<float> pos;
        private Vector<float> vel;

        public ParticleSystem(int count) {
            PrintFlag("Vector<float> Support", Vector<float>.IsSupported);
            PrintFlag("Vector128 Hardware Accel", Vector128.IsHardwareAccelerated);
            PrintFlag("Vector256 Hardware Accel", Vector256.IsHardwareAccelerated);
            PrintFlag("Vector512 Hardware Accel", Vector512.IsHardwareAccelerated);

            var tc = count * 3;

            this.pos = new(new float[tc]);
            
            var vel = new float[tc];
            for( var i = 0; i < tc; ++i) {
                vel[i] = 1f;
            }
            this.vel = new(vel);
        }

        #region IParticleSystem

        public void Update(float dt) {
            // This is hilariously simple ;)
            //
            this.pos += vel * dt;
        }

        public (float, float, float) GetPosition(int index) {
            var real_idx = index * 3;

            var x = this.pos.GetElement(real_idx + 0);
            var y = this.pos.GetElement(real_idx + 1);
            var z = this.pos.GetElement(real_idx + 2);

            return (x, y, z);
        }

        #endregion IParticleSystem

        void PrintFlag(string title, bool on) {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"    {title}: ");
            Console.ForegroundColor = on ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(on);
            Console.ForegroundColor = ConsoleColor.Gray;
        }    
    }
}