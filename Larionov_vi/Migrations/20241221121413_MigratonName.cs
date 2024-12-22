using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Larionov_vi.Migrations
{
    public partial class MigratonName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Группы",
                columns: table => new
                {
                    КодГруппы = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеГруппы = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Специализация = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ГодКурса = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Группы__C53084C65915E8B2", x => x.КодГруппы);
                });

            migrationBuilder.CreateTable(
                name: "Роли",
                columns: table => new
                {
                    КодРоли = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеРоли = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Роли__A2517212851D6471", x => x.КодРоли);
                });

            migrationBuilder.CreateTable(
                name: "События",
                columns: table => new
                {
                    КодСобытия = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеСобытия = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ДатаСобытия = table.Column<DateTime>(type: "date", nullable: false),
                    МестоПроведения = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Описание = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__События__D78AD13A7A39964B", x => x.КодСобытия);
                });

            migrationBuilder.CreateTable(
                name: "Факультеты",
                columns: table => new
                {
                    КодФакультета = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеФакультета = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Декан = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Факульте__7E28CC4CAAE2088C", x => x.КодФакультета);
                });

            migrationBuilder.CreateTable(
                name: "Пользователи",
                columns: table => new
                {
                    КодПользователя = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Имя = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Фамилия = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ЭлектроннаяПочта = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ХэшПароля = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    КодРоли = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Пользова__200434A2F1FCAB7E", x => x.КодПользователя);
                    table.ForeignKey(
                        name: "FK__Пользоват__КодРо__6383C8BA",
                        column: x => x.КодРоли,
                        principalTable: "Роли",
                        principalColumn: "КодРоли");
                });

            migrationBuilder.CreateTable(
                name: "Программы",
                columns: table => new
                {
                    КодПрограммы = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеПрограммы = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    КодФакультета = table.Column<int>(type: "int", nullable: false),
                    Продолжительность = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Программ__8D5DBFABF25D6D56", x => x.КодПрограммы);
                    table.ForeignKey(
                        name: "FK__Программы__КодФа__693CA210",
                        column: x => x.КодФакультета,
                        principalTable: "Факультеты",
                        principalColumn: "КодФакультета");
                });

            migrationBuilder.CreateTable(
                name: "Курсы",
                columns: table => new
                {
                    КодКурса = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    НазваниеКурса = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Описание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Кредиты = table.Column<int>(type: "int", nullable: false),
                    КодПреподавателя = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Курсы__5E122F8D19B1343B", x => x.КодКурса);
                    table.ForeignKey(
                        name: "FK__Курсы__КодПрепод__72C60C4A",
                        column: x => x.КодПреподавателя,
                        principalTable: "Пользователи",
                        principalColumn: "КодПользователя");
                });

            migrationBuilder.CreateTable(
                name: "Студенты",
                columns: table => new
                {
                    КодСтудента = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодПользователя = table.Column<int>(type: "int", nullable: false),
                    ГодПоступления = table.Column<int>(type: "int", nullable: false),
                    КодГруппы = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Студенты__4036A07509264F9C", x => x.КодСтудента);
                    table.ForeignKey(
                        name: "FK__Студенты__КодГру__6FE99F9F",
                        column: x => x.КодГруппы,
                        principalTable: "Группы",
                        principalColumn: "КодГруппы");
                    table.ForeignKey(
                        name: "FK__Студенты__КодПол__6EF57B66",
                        column: x => x.КодПользователя,
                        principalTable: "Пользователи",
                        principalColumn: "КодПользователя");
                });

            migrationBuilder.CreateTable(
                name: "УчастникиСобытий",
                columns: table => new
                {
                    КодУчастника = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодПользователя = table.Column<int>(type: "int", nullable: false),
                    КодСобытия = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Участник__1D4507D0D8174F9C", x => x.КодУчастника);
                    table.ForeignKey(
                        name: "FK__Участники__КодПо__0C85DE4D",
                        column: x => x.КодПользователя,
                        principalTable: "Пользователи",
                        principalColumn: "КодПользователя");
                    table.ForeignKey(
                        name: "FK__Участники__КодСо__0D7A0286",
                        column: x => x.КодСобытия,
                        principalTable: "События",
                        principalColumn: "КодСобытия");
                });

            migrationBuilder.CreateTable(
                name: "Задания",
                columns: table => new
                {
                    КодЗадания = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодКурса = table.Column<int>(type: "int", nullable: false),
                    Название = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Описание = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ДатаСдачи = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Задания__1AB5BEBC25EA9CA3", x => x.КодЗадания);
                    table.ForeignKey(
                        name: "FK__Задания__КодКурс__03F0984C",
                        column: x => x.КодКурса,
                        principalTable: "Курсы",
                        principalColumn: "КодКурса");
                });

            migrationBuilder.CreateTable(
                name: "Занятия",
                columns: table => new
                {
                    КодЗанятия = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодКурса = table.Column<int>(type: "int", nullable: false),
                    ДатаЗанятия = table.Column<DateTime>(type: "date", nullable: false),
                    ВремяНачала = table.Column<TimeSpan>(type: "time", nullable: false),
                    ВремяОкончания = table.Column<TimeSpan>(type: "time", nullable: false),
                    Аудитория = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Занятия__8CA853B4DDE2107E", x => x.КодЗанятия);
                    table.ForeignKey(
                        name: "FK__Занятия__КодКурс__7D439ABD",
                        column: x => x.КодКурса,
                        principalTable: "Курсы",
                        principalColumn: "КодКурса");
                });

            migrationBuilder.CreateTable(
                name: "ПрограммыКурсов",
                columns: table => new
                {
                    КодПрограммыКурса = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодКурса = table.Column<int>(type: "int", nullable: false),
                    КодПрограммы = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Программ__4FC961AA7A903F7C", x => x.КодПрограммыКурса);
                    table.ForeignKey(
                        name: "FK__Программы__КодКу__75A278F5",
                        column: x => x.КодКурса,
                        principalTable: "Курсы",
                        principalColumn: "КодКурса");
                    table.ForeignKey(
                        name: "FK__Программы__КодПр__76969D2E",
                        column: x => x.КодПрограммы,
                        principalTable: "Программы",
                        principalColumn: "КодПрограммы");
                });

            migrationBuilder.CreateTable(
                name: "Зачисления",
                columns: table => new
                {
                    КодЗачисления = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодСтудента = table.Column<int>(type: "int", nullable: false),
                    КодКурса = table.Column<int>(type: "int", nullable: false),
                    ДатаЗачисления = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Зачислен__2D95BA62E9DF52BF", x => x.КодЗачисления);
                    table.ForeignKey(
                        name: "FK__Зачислени__КодКу__7A672E12",
                        column: x => x.КодКурса,
                        principalTable: "Курсы",
                        principalColumn: "КодКурса");
                    table.ForeignKey(
                        name: "FK__Зачислени__КодСт__797309D9",
                        column: x => x.КодСтудента,
                        principalTable: "Студенты",
                        principalColumn: "КодСтудента");
                });

            migrationBuilder.CreateTable(
                name: "Работы",
                columns: table => new
                {
                    КодРаботы = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодЗадания = table.Column<int>(type: "int", nullable: false),
                    КодСтудента = table.Column<int>(type: "int", nullable: false),
                    ДатаСдачи = table.Column<DateTime>(type: "date", nullable: false),
                    Оценка = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Комментарии = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Работы__76A3F6880D1CB017", x => x.КодРаботы);
                    table.ForeignKey(
                        name: "FK__Работы__КодЗадан__06CD04F7",
                        column: x => x.КодЗадания,
                        principalTable: "Задания",
                        principalColumn: "КодЗадания");
                    table.ForeignKey(
                        name: "FK__Работы__КодСтуде__07C12930",
                        column: x => x.КодСтудента,
                        principalTable: "Студенты",
                        principalColumn: "КодСтудента");
                });

            migrationBuilder.CreateTable(
                name: "Посещаемость",
                columns: table => new
                {
                    КодПосещаемости = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    КодЗанятия = table.Column<int>(type: "int", nullable: false),
                    КодСтудента = table.Column<int>(type: "int", nullable: false),
                    СтатусПосещаемости = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Посещаем__40245443CD5E7E20", x => x.КодПосещаемости);
                    table.ForeignKey(
                        name: "FK__Посещаемо__КодЗа__00200768",
                        column: x => x.КодЗанятия,
                        principalTable: "Занятия",
                        principalColumn: "КодЗанятия");
                    table.ForeignKey(
                        name: "FK__Посещаемо__КодСт__01142BA1",
                        column: x => x.КодСтудента,
                        principalTable: "Студенты",
                        principalColumn: "КодСтудента");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Группы__AC501ED3E29F7299",
                table: "Группы",
                column: "НазваниеГруппы",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Задания_КодКурса",
                table: "Задания",
                column: "КодКурса");

            migrationBuilder.CreateIndex(
                name: "IX_Занятия_КодКурса",
                table: "Занятия",
                column: "КодКурса");

            migrationBuilder.CreateIndex(
                name: "IX_Зачисления_КодКурса",
                table: "Зачисления",
                column: "КодКурса");

            migrationBuilder.CreateIndex(
                name: "IX_Зачисления_КодСтудента",
                table: "Зачисления",
                column: "КодСтудента");

            migrationBuilder.CreateIndex(
                name: "IX_Курсы_КодПреподавателя",
                table: "Курсы",
                column: "КодПреподавателя");

            migrationBuilder.CreateIndex(
                name: "IX_Пользователи_КодРоли",
                table: "Пользователи",
                column: "КодРоли");

            migrationBuilder.CreateIndex(
                name: "UQ__Пользова__372226199806A638",
                table: "Пользователи",
                column: "ЭлектроннаяПочта",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Посещаемость_КодЗанятия",
                table: "Посещаемость",
                column: "КодЗанятия");

            migrationBuilder.CreateIndex(
                name: "IX_Посещаемость_КодСтудента",
                table: "Посещаемость",
                column: "КодСтудента");

            migrationBuilder.CreateIndex(
                name: "IX_Программы_КодФакультета",
                table: "Программы",
                column: "КодФакультета");

            migrationBuilder.CreateIndex(
                name: "IX_ПрограммыКурсов_КодКурса",
                table: "ПрограммыКурсов",
                column: "КодКурса");

            migrationBuilder.CreateIndex(
                name: "IX_ПрограммыКурсов_КодПрограммы",
                table: "ПрограммыКурсов",
                column: "КодПрограммы");

            migrationBuilder.CreateIndex(
                name: "IX_Работы_КодЗадания",
                table: "Работы",
                column: "КодЗадания");

            migrationBuilder.CreateIndex(
                name: "IX_Работы_КодСтудента",
                table: "Работы",
                column: "КодСтудента");

            migrationBuilder.CreateIndex(
                name: "UQ__Роли__B867938ED5819691",
                table: "Роли",
                column: "НазваниеРоли",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Студенты_КодГруппы",
                table: "Студенты",
                column: "КодГруппы");

            migrationBuilder.CreateIndex(
                name: "IX_Студенты_КодПользователя",
                table: "Студенты",
                column: "КодПользователя");

            migrationBuilder.CreateIndex(
                name: "IX_УчастникиСобытий_КодПользователя",
                table: "УчастникиСобытий",
                column: "КодПользователя");

            migrationBuilder.CreateIndex(
                name: "IX_УчастникиСобытий_КодСобытия",
                table: "УчастникиСобытий",
                column: "КодСобытия");

            migrationBuilder.CreateIndex(
                name: "UQ__Факульте__58EF202699661899",
                table: "Факультеты",
                column: "НазваниеФакультета",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Зачисления");

            migrationBuilder.DropTable(
                name: "Посещаемость");

            migrationBuilder.DropTable(
                name: "ПрограммыКурсов");

            migrationBuilder.DropTable(
                name: "Работы");

            migrationBuilder.DropTable(
                name: "УчастникиСобытий");

            migrationBuilder.DropTable(
                name: "Занятия");

            migrationBuilder.DropTable(
                name: "Программы");

            migrationBuilder.DropTable(
                name: "Задания");

            migrationBuilder.DropTable(
                name: "Студенты");

            migrationBuilder.DropTable(
                name: "События");

            migrationBuilder.DropTable(
                name: "Факультеты");

            migrationBuilder.DropTable(
                name: "Курсы");

            migrationBuilder.DropTable(
                name: "Группы");

            migrationBuilder.DropTable(
                name: "Пользователи");

            migrationBuilder.DropTable(
                name: "Роли");
        }
    }
}
