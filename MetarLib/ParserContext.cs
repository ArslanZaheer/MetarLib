namespace MetarLib
{
    public class ParserContext
    {
        private readonly Metar _metar;
        
        public ParserContext()
        {
            _metar = new Metar();

            Metar = _metar;
        }
        
        /// <summary>
        /// The METAR that is currently being parsed.
        /// </summary>
        public Metar Metar { get; private set; }
        public decimal? Probability { get; set; }

        public void ParseBecoming()
        {
            var becomingMetar = new TemporaryMetar(_metar, Probability);
            Probability = null;
            
            _metar.Becoming.Add(becomingMetar);
            Metar = becomingMetar;
        }
        
        public void ParseTemporary()
        {
            var temporaryMetar = new TemporaryMetar(_metar, Probability);
            Probability = null;
            
            _metar.Temporary.Add(temporaryMetar);
            Metar = temporaryMetar;
        }
        
        /// <summary>
        /// The final METAR message.
        /// </summary>
        /// <returns>The result of parsing a full METAR message.</returns>
        public Metar GetResult() => _metar;
    }
}