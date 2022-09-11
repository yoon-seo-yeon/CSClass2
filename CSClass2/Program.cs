using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSClass2
{
    internal class Program

    {
        static void NextPos(int x, int y, int vx, int vy, out int rx, out int ry)
        {
            rx = x + vx;
            ry = y + vy;
        }
        class PointClass
        {
            public int x;
            public int y;
            public PointClass(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        struct PointStruct
        {
            public int x;
            public int y;

            public PointStruct(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

        }

        struct Point
        {
            public int x;
            public int y;
            public string testA;
            public string testB;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.testA = "초기화";
                this.testB = "초기화";
            }

            public Point(int x, int y, string s)
            {
                this.x = x;
                this.y = y;
                this.testA = s;
                this.testB = s;
            }
        }

        static void Main(string[] args)
        {
            Wanted<string> wantedString = new Wanted<string>("String");
            Wanted<int> wantedInt = new Wanted<int>(52273);
            Wanted<double> wantedDouble = new Wanted<double>(52.273);

            Console.WriteLine(wantedString.Value);
            Console.WriteLine(wantedInt.Value);
            Console.WriteLine(wantedDouble.Value);

            Products p = new Products();
            Console.WriteLine("오늘의 점심 메뉴는 " + p[2] + "입니다.");
            p[2] = "단무지";
            Console.WriteLine("오늘의 점심 메뉴는 " + p[2] + "입니다.");

            Console.WriteLine("숫자 입력 : ");
            //int output;
            bool result = int.TryParse(Console.ReadLine(), out int output);
            if (result)
            {
                Console.WriteLine("입력한 숫자 " + output);
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요!");
            }

            int x = 0;
            int y = 0;
            int vx = 1;
            int vy = 1;
            Console.WriteLine("현재 좌표 : (" + x + ", " + y + ")");
            NextPos(x, y, vx, vy, out x, out y);
            Console.WriteLine("다음 좌표 : (" + x + ", " + y + ")");

            Point point;
            point.x = 10;
            point.y = 10;
            point = new Point(100, 200);
            point = new Point();
            Console.WriteLine(point.x + " / " + point.y);

            PointClass pcA = new PointClass(10, 20);
            PointClass pcB = pcA;
            pcB.x = 100; pcB.y = 200;
            Console.WriteLine(pcA.x + " / " + pcA.y);
            Console.WriteLine(pcB.x + " / " + pcB.y);

            PointStruct psA = new PointStruct(10, 20);
            PointStruct psB = psA;
            psB.x = 100; psB.y = 200;
            Console.WriteLine(psA.x + " / " + psA.y);
            Console.WriteLine(psB.x + " / " + psB.y);

            using(Dummy dummy = new Dummy())
            {
                List<Product> list = new List<Product>()
                {
                    new Product(){Name="고구마", Price=1500},
                    new Product(){Name="사과", Price=2400},
                    new Product(){Name="바나나", Price=1000},
                    new Product(){Name="배", Price=3000},
                };
                list.Sort();

                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }

            IBasic test = new TestClass();
            test.TestProperty = 3;
            test.TestInstanceMethod();
            //test.foobar(); //<-인터페이스에서는 객체로 활동하지 못한다
            (test as TestClass).foobar(); //<-이렇게는 됨

            Child c = new Child();
            Parent childAsParent = c;
            IDisposable childAsDispoable = c;
            IComparable<Child> childAsComparable = c;

            File.WriteAllText(@"c:\TEMP\test.txt", "문자열 메시지를 씁니다");
            Console.WriteLine(File.ReadAllText(@"c:\TEMP\test.txt"));

            using (StreamWriter writer = new StreamWriter(@"c:\TEMP\test.txt"))
            {
                writer.WriteLine("안녕하세요");
                writer.WriteLine("StreamWriter 클래스를 사용해");
                writer.Write("글자를");
                writer.Write("여러줄");
                writer.Write("입력해봅니다.");

                for(int i = 0; i < 100; i++)
                {
                    writer.WriteLine("반복문 - " + i);
                }
            }

            using (StreamReader reader = new StreamReader(@"c:\temp\test.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            //예외
            string[] array = { "가", "나" };
            Boolean isInputLoop = true;

            while(isInputLoop)
            {
                Console.WriteLine("인덱스 범위를 넘었습니다.");
                Console.WriteLine("숫자를 입력해주세요.[0~" + (array.Length - 1) + "]:");

                string input = Console.ReadLine();
                try
                {
                    int index = int.Parse(input);
                    Console.WriteLine("입력한 위치의 값은 '" + array[index] + "'입니다.");
                    isInputLoop = false;
                }
                catch(FormatException exception)
                {
                    Console.WriteLine("이런, 숫자가 아닌 것을 입력하셨군요!");
                }
                catch(IndexOutOfRangeException exception)
                {
                    Console.WriteLine("이런, 0 이상 " + array.Length + " 미만 값을 입력하세요!");
                }
                catch (Exception exception)
                {
                    Console.WriteLine("이런, 알 수 예외가 발생했군요.");
                }
                finally
                {
                    Console.WriteLine("프로그램이 종료되었습니다.");
                }
            }
        }

        class Dummy : IDisposable
        { 
            public void Dispose()
            {
                Console.WriteLine("Dispose() 메서드를 호출했습니다.");
            }
        }

        class TestClass : IBasic
        {
            public int foobar()
            {
                return -1;
            }
            public int TestProperty 
            { 
                get => throw new NotImplementedException(); 
                set => throw new NotImplementedException(); 
            }

            public int TestInstanceMethod()
            {
                throw new NotImplementedException();
            }
        }
    }
}
