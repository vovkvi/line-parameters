namespace LineParameters
{
    class vl
    {
        public string Name { get; set; }
        public double R { get; set; }
        public double X35 { get; set; }
        public double X110 { get; set; }
        public double X220 { get; set; }
        public double X330 { get; set; }
        public double B35 { get; set; }
        public double B110 { get; set; }
        public double B220 { get; set; }
        public double B330 { get; set; }

        public vl(string name = null, double r = 0.0, double x35 = 0.0, double x110 = 0.0, double x220 = 0.0, double x330 = 0.0, double b35 = 0.0, double b110 = 0.0, double b220 = 0.0, double b330 = 0.0) 
        {
            Name = name;
            R = r;
            X35 = x35;
            X110 = x110;
            X220 = x220;
            X330 = x330;
            B35 = b35;
            B110 = b110;
            B220 = b220;
            B330 = b330;
        }
    }
}
