namespace GridPerfTest.ViewModel;

public sealed class SourceItem
{
    public SourceItem()
    {
        _values = Enumerable.Range(0, 64).Select(_ => new ItemValue(Random.Shared.NextDouble())).ToArray();
    }

    private readonly ItemValue[] _values;

    public ItemValue Value00 => _values[0];
    public ItemValue Value01 => _values[1];
    public ItemValue Value02 => _values[2];
    public ItemValue Value03 => _values[3];
    public ItemValue Value04 => _values[4];
    public ItemValue Value05 => _values[5];
    public ItemValue Value06 => _values[6];
    public ItemValue Value07 => _values[7];
    public ItemValue Value08 => _values[8];
    public ItemValue Value09 => _values[9];

    public ItemValue Value10 => _values[10];
    public ItemValue Value11 => _values[11];
    public ItemValue Value12 => _values[12];
    public ItemValue Value13 => _values[13];
    public ItemValue Value14 => _values[14];
    public ItemValue Value15 => _values[15];
    public ItemValue Value16 => _values[16];
    public ItemValue Value17 => _values[17];
    public ItemValue Value18 => _values[18];
    public ItemValue Value19 => _values[19];

    public ItemValue Value20 => _values[20];
    public ItemValue Value21 => _values[21];
    public ItemValue Value22 => _values[22];
    public ItemValue Value23 => _values[23];
    public ItemValue Value24 => _values[24];
    public ItemValue Value25 => _values[25];
    public ItemValue Value26 => _values[26];
    public ItemValue Value27 => _values[27];
    public ItemValue Value28 => _values[28];
    public ItemValue Value29 => _values[29];

    public ItemValue Value30 => _values[30];
    public ItemValue Value31 => _values[31];
    public ItemValue Value32 => _values[32];
    public ItemValue Value33 => _values[33];
    public ItemValue Value34 => _values[34];
    public ItemValue Value35 => _values[35];
    public ItemValue Value36 => _values[36];
    public ItemValue Value37 => _values[37];
    public ItemValue Value38 => _values[38];
    public ItemValue Value39 => _values[39];

    public ItemValue Value40 => _values[40];
    public ItemValue Value41 => _values[41];
    public ItemValue Value42 => _values[42];
    public ItemValue Value43 => _values[43];
    public ItemValue Value44 => _values[44];
    public ItemValue Value45 => _values[45];
    public ItemValue Value46 => _values[46];
    public ItemValue Value47 => _values[47];
    public ItemValue Value48 => _values[48];
    public ItemValue Value49 => _values[49];

    public ItemValue Value50 => _values[50];
    public ItemValue Value51 => _values[51];
    public ItemValue Value52 => _values[52];
    public ItemValue Value53 => _values[53];
    public ItemValue Value54 => _values[54];
    public ItemValue Value55 => _values[55];
    public ItemValue Value56 => _values[56];
    public ItemValue Value57 => _values[57];
    public ItemValue Value58 => _values[58];
    public ItemValue Value59 => _values[59];

    public ItemValue Value60 => _values[60];
    public ItemValue Value61 => _values[61];
    public ItemValue Value62 => _values[62];
    public ItemValue Value63 => _values[63];
}