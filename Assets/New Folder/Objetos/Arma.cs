namespace Objetos
{
    enum TipoDeArma { Base, Tridireccional, OctaDireccional }

    struct Arma
    {
        public TipoDeArma TipoDeArma { get; set; }
        public float Daño { get; set; }
        public float VelocidadDeAtaque { get; set; }
        public float Recalentamiento { get; set; }
        public float MaxRecalentamiento { get; set; }
    }
}
