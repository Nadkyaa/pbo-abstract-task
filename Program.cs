// Ketentuan Nomor 1
public abstract class Robot
{
    public string nama;
    public int energi;
    public int armor;
    public int serangan;

    public Robot(string nama, int energi, int armor, int serangan)
    {
        this.nama = nama;
        this.energi = energi;
        this.armor = armor;
        this.serangan = serangan;
    }

    public void Serang(Robot target, int damage)
    {
        int totalDamage = damage - target.armor;
        if (totalDamage > 0)
        {
            target.energi -= totalDamage;
            Console.WriteLine(nama + " Nyerang Jrog " + target.nama + " Memakai Skill Bulan " + damage + " Hiyaaaaaaaaaaaa");
        }
        else
        {
            Console.WriteLine(nama + " Aku Capek Mas " + target.nama + " Armormu Kuat Anjay");
        }
    }

    public abstract void GunakanKemampuan(Kemampuan kemampuan);

    public void CetakInformasi()
    {
        Console.WriteLine("Nama: " + nama);
        Console.WriteLine("Energi: " + energi);
        Console.WriteLine("Armor: " + armor);
        Console.WriteLine("Serangan: " + serangan);
    }

    internal void Serang(Robot robot2)
    {
        throw new NotImplementedException();
    }
}

// INTERFACE: KEMAMPUAN
public interface Kemampuan
{
    void Gunakan(Robot robot);
}

// KEMAMPUAN
public class GunakanKemampuan : Kemampuan
{
    public void Gunakan(Robot robot)
    {
        Console.WriteLine(robot.nama + " menggunakan kemampuan ndamasuk akal!");
        robot.energi += 10;
    }
}

// SUB ROBOT
public class RobotData : Robot
{
    public RobotData(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan)
    {
    }

    public override void GunakanKemampuan(Kemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }
}

// BOS ROBOT
public class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan)
    {
        this.energi *= 2; 
        this.armor *= 2; 
        this.serangan *= 2;
    }

    public override void GunakanKemampuan(Kemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }
}

// KEMAMPUAN TAMBAHAN
public abstract class KemampuanTambahan
{
    protected int cooldown;
    protected int timer;

    public void Update()
    {
        if (timer > 0)
        {
            timer--;
        }
    }

    public bool IsReady()
    {
        return timer == 0;
    }

    public abstract void Gunakan(Robot robot, Robot target);
}

public class Perbaikan : KemampuanTambahan
{
    public Perbaikan()
    {
        cooldown = 5;
        timer = 0;
    }

    public override void Gunakan(Robot robot, Robot target)
    {
        if (IsReady())
        {
            robot.energi += 10; // Memulihkan energi robot
            Console.WriteLine(robot.nama + " Bentarr repairr duluu");
            timer = cooldown;
        }
        else
        {
            Console.WriteLine(robot.nama + " Sabarrr nanti lagi");
        }
    }
}

public class SeranganListrik : KemampuanTambahan
{
    public SeranganListrik()
    {
        cooldown = 3;
        timer = 0;
    }

    public override void Gunakan(Robot robot, Robot target)
    {
        if (IsReady())
        {
            target.energi -= 10; 
            Console.WriteLine(robot.nama + " Takkk Setrummmm kamooo");
            timer = cooldown;
        }
        else
        {
            Console.WriteLine(robot.nama + " Sabarr Ngecass");
        }
    }
}

public class SeranganPlasma : KemampuanTambahan
{
    public SeranganPlasma()
    {
        cooldown = 10;
        timer = 0;
    }

    public override void Gunakan(Robot robot, Robot target)
    {
        if (IsReady())
        {
            target.energi -= 20; 
            Console.WriteLine(robot.nama + " Takk Seranggg Plasmaaaa");
            timer = cooldown;
        }
        else
        {
            Console.WriteLine(robot.nama + " Sabarrr Restocckkk");
        }
    }
}

public class PertahananSuper : KemampuanTambahan
{
    public PertahananSuper()
    {
        cooldown = 8;
        timer = 0;
    }

    public override void Gunakan(Robot robot, Robot target)
    {
        if (IsReady())
        {
            robot.armor += 5;
            Console.WriteLine(robot.nama + " Bertahann sekuatt tenagaaa");
            timer = cooldown;
        }
        else
        {
            Console.WriteLine(robot.nama + " Sabarrr Ngko manehhhhhhh");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Buat robot
        Robot robot1 = new RobotData("Robot 1", 100, 50, 20);
        Robot robot2 = new BosRobot("Bos Robot", 200, 100, 40);

        // Buat kemampuan tambahan
        KemampuanTambahan perbaikan = new Perbaikan();
        KemampuanTambahan seranganListrik = new SeranganListrik();
        KemampuanTambahan seranganPlasma = new SeranganPlasma();
        KemampuanTambahan pertahananSuper = new PertahananSuper();

        // Jalankan permainan
        while (robot1.energi > 0 && robot2.energi > 0)
        {
            Console.WriteLine("Giliran Robot 1:");
            robot1.CetakInformasi();
            Console.WriteLine("Pilih kemampuan:");
            Console.WriteLine("1. Perbaikan");
            Console.WriteLine("2. Serangan Listrik");
            Console.WriteLine("3. Serangan Plasma");
            Console.WriteLine("4. Pertahanan Super");
            int pilihan = Convert.ToInt32(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    perbaikan.Gunakan(robot1, robot2);
                    break;
                case 2:
                    seranganListrik.Gunakan(robot1, robot2);
                    break;
                case 3:
                    seranganPlasma.Gunakan(robot1, robot2);
                    break;
                case 4:
                    pertahananSuper.Gunakan(robot1, robot2);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid");
                    break;
            }

            Console.WriteLine("Giliran Bos Robot:");
            robot2.CetakInformasi();
            robot2.Serang(robot1, 40);

            // Update cooldown kemampuan
            perbaikan.Update();
            seranganListrik.Update();
            seranganPlasma.Update();
            pertahananSuper.Update();
        }

        // Cetak hasil permainan
        if (robot1.energi > 0)
        {
            Console.WriteLine("Robot 1 menang!");
        }
        else
        {
            Console.WriteLine("Bos Robot menang!");
        }
    }
}