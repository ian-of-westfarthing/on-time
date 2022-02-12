using System;
using System.Threading;

namespace ontime.Game
{
    public static class Gen
    {
        public static Random rand = new Random((int)DateTime.Now.Ticks);

        public static void Seed(int tm = 0)
        {
            long s = rand.Next();

            for(int a = 0; a < 25; a++)
            {
                s += DateTime.Now.Ticks * rand.Next();
                Thread.Sleep(5);
            }

            for(int a = 0; a < 8000; a++)
            {
                s += rand.Next();
                s += DateTime.Now.Ticks * rand.Next();
            }

            s += Environment.WorkingSet;
            s += Environment.CommandLine.Length;
            s += Environment.CurrentDirectory.Length;
            s += Environment.CurrentManagedThreadId;
            s += Environment.MachineName.Length;
            s += Environment.TickCount;
            s += Environment.Version.Build;
            s += (int)Environment.OSVersion.Platform;
            s += Environment.OSVersion.ServicePack.Length;

            rand = new Random((int)s);

            if(tm < 5)
            {
                Console.WriteLine("          " + s);
                Seed(tm + 1);
            }
        }

        public static string Name()
        {
            string name = "";

            string[] s = "ku,dona ,gghnom ,thuror ,throsi ,finik ,filiki ,floki ,samuel ,do,f,tarn ,qu,kul,ji ,babn,yol,yas ,yak ,yis ,yia ,bab,ba,wi,wia ,ro,rok ,roy ,uu,á,i,ghai,ijhi,tai ,yasmine ,oden ,zues ,suse ,iann ,jon ,liakki ,yonmic ,mica ,bronsze ,irn ,oinn ,bomburn ,west,south,north,east,fro,for,finj,rfo".Split(',');
            string[] m = "da fhuor ges gest jes ka kin kan ken kis kes kas lak lok lik lea ti to tim am qu bohm ghigi".Split();
            string[] e = "ga mak maki hah hahm bahm lahm ims som sam igig aka don ál nino yakn dan kik lon to lo to l bom tomak tie lop ia jai ghi dai".Split();


            name += s[rand.Next(0, s.Length)];

            for(int a = 0; a < 2; a++)
            {
                name += m[rand.Next(0, m.Length)];
            }

            name += e[rand.Next(0, e.Length)];

            return name;
        }

        public static string LegendaryName(bool person)
        {
            string nm = Name();

            if(person && rand.Next(0, 5) == 0)
            {
                nm += " of " + Name();
            }
            else
            {
                nm += " the ";

                string[] legend = "trodden,horse,bear,coyote,evil,great,horns,leg,beard,splint,splinter,ice,homes,book,bookshelf,garden,axe,pickaxe,steel,key,rodent,awesome,kicks,wrestle,home,trail,cover,rock,tree,gnome,elf,goblin,slayer,eater,biter,person,fern,plant,stove,iron,gold,steel,mansion,loaf,carrot,steak,leaves".Split(',');

                nm += legend[rand.Next(0, legend.Length)];
                nm += legend[rand.Next(0, legend.Length)];
            }


            return nm;
        }
    }
}
