#pragma warning disable IDE0003 // disable lint for 'this' qualification

namespace SimpleParticles {
    struct Vec3 {
        public float x;
        public float y;
        public float z;

        public Vec3() {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Vec3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Random(float min, float max) {
            var range = max - min;
            this.x = System.Random.Shared.NextSingle() * range + min;
            this.y = System.Random.Shared.NextSingle() * range + min;
            this.z = System.Random.Shared.NextSingle() * range + min;
        }

        public static Vec3 operator* (Vec3 v, float s) {
            return new Vec3(v.x * s, v.y * s, v.z * s);
        }

        public static Vec3 operator+ (Vec3 l, Vec3 r) {
            return new Vec3(l.x + r.x, l.y + r.y, l.z + r.z);
        }
    }

    struct Particle {
        public Vec3 pos;
        public Vec3 vel;

        public Particle() {
            this.pos = new Vec3();
            this.vel = new Vec3();
        }
    }

    class ParticleSystem : IParticleSystem {
        public List<Particle> particles = new();

        public ParticleSystem(int count) {
            foreach( var i in Enumerable.Range(0, count) ) {
                var p = new Particle();
                p.vel.Random(-1, 1);
                this.particles.Add(p);
            }
        }

        public void Update(float dt) {
            // Console.Write($" [{this.particles[0].pos.x}, {this.particles[0].pos.y}, {this.particles[0].pos.z}]");

            for( var i = 0; i < this.particles.Count; i++ ) {
                var p = this.particles[i];
                p.pos += p.vel * dt;
                this.particles[i] = p;
            }

            // Console.Write($" + [{this.particles[0].vel.x}, {this.particles[0].vel.y}, {this.particles[0].vel.z}]");
            // Console.WriteLine($" == [{this.particles[0].pos.x}, {this.particles[0].pos.y}, {this.particles[0].pos.z}]");
        }
    }
}