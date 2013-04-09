using System;

namespace NumberGenerator.Lib.Personal
{
    public class Ssn
    {
        public string Value { get; private set; }
        public int D1 { get; private set; }
        public int D2 { get; private set; }
        public int M1 { get; private set; }
        public int M2 { get; private set; }
        public int Y1 { get; private set; }
        public int Y2 { get; private set; }
        public int I1 { get; private set; }
        public int I2 { get; private set; }
        public int I3 { get; private set; }
        public int K1 { get; private set; }
        public int K2 { get; private set; }
        public Gender Gender { get { return I3 % 2 == 0 ? Gender.Male : Gender.Female; } }
        public bool IsValid
        {
            get
            {
                try
                {
                    Validate();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void Validate()
        {
            var k1 = CalculateK1();
            if (k1 == 10 || k1 != K1)
            {
                throw new Exception("Invalid K1");
            }
            int k2 = 11 - (5 * D1 + 4 * D2 + 3 * M1 + 2 * M2 + 7 * Y1 + 6 * Y2 + 5 * I1 + 4 * I2 + 3 * I3 + 2 * K1) % 11;
            if (k2 == 11)
            {
                k2 = 0;
            }
            if (k2 == 10 || k2 != K2)
            {
                throw new Exception("Invalid K2");
            }
        }

        public int CalculateK1()
        {
            var k1 = 11 - (3 * D1 + 7 * D2 + 6 * M1 + 1 * M2 + 8 * Y1 + 9 * Y2 + 4 * I1 + 5 * I2 + 2 * I3) % 11;
            return k1 == 11 ? 0 : k1;
        }

        public Ssn(string value)
        {
            Value = value;
            D1 = ValueOrDefault(value, 0);
            D2 = ValueOrDefault(value, 1);
            M1 = ValueOrDefault(value, 2);
            M2 = ValueOrDefault(value, 3);
            Y1 = ValueOrDefault(value, 4);
            Y2 = ValueOrDefault(value, 5);
            I1 = ValueOrDefault(value, 6);
            I2 = ValueOrDefault(value, 7);
            I3 = ValueOrDefault(value, 8);
            K1 = ValueOrDefault(value, 9);
            K2 = ValueOrDefault(value, 10);
        }

        private static int ValueOrDefault(string value, int index)
        {
            try
            {
                return Convert.ToInt32(value[index]);
            }
            catch
            {
                return 0;
            }
        }
    }
}