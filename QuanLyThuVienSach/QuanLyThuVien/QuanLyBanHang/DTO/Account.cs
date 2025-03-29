using System.Data;

public class Account
{
    private int iD;
    private string tenHienThi;
    private string tenDangNhap;
    private string matKhau;
    private int quyenHan;

    public Account(int iD, string tenHienThi, string tenDangNhap, string matKhau, int quyenHan)
    {
        this.TenHienThi = tenHienThi;
        this.TenDangNhap = tenDangNhap;
        this.MatKhau = matKhau;
        this.QuyenHan = quyenHan;
        this.ID = iD;
    }

    public Account(DataRow row)
    {
        this.TenHienThi = row["tenHienThi"].ToString();
        this.TenDangNhap = row["tenDangNhap"].ToString();
        this.MatKhau = row["matKhau"].ToString();
        this.QuyenHan = (int)row["quyenHan"];
        this.ID = (int)row["iD"];
    }

    public string TenHienThi 
    {
        get { return tenHienThi; }
        set { tenHienThi = value; }
    }

    public string TenDangNhap
    {
        get { return tenDangNhap; }
        set { tenDangNhap = value; }
    }

    public string MatKhau
    {
        get { return matKhau; }
        set { matKhau = value; }
    }

    public int QuyenHan
    {
        get { return quyenHan; }
        set { quyenHan = value; }
    }

    public int ID
    {
        get { return iD; }
        set { iD = value; }
    }
}
