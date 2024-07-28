public interface IParticleSystem {
    void Update(float dt);
    (float, float, float) GetPosition(int index);
}