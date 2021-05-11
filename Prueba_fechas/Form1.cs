using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prueba_fechas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DateTime Fecha_termino_ult_ticket;
            DateTime fecha_devuelta;
            DateTime Fecha_reg_tckt_actual = DateTime.Parse("06/09/2019 15:42:01");//hora de registro del ticket
            Fecha_termino_ult_ticket = DateTime.Parse("06/09/2019 11:00:01");//fecha de termino del ticket anterior

            fecha_devuelta = Devolver_fecha_prevista(Fecha_reg_tckt_actual, Fecha_termino_ult_ticket);
            MessageBox.Show(fecha_devuelta.ToString());

        }
        private DateTime Devolver_fecha_prevista(DateTime Fecha_reg_tckt_actual, DateTime Fecha_termino_ult_ticket)
        {   // el parametro Fecha_termino_ult_ticket=fecha y hora en que los libros electonicos del ticket anterior terminaron de procesarse
            // el parametro fecha_devuelta= promesa de entrega de informacion por codigo de ticket actual registrado
            //el inicio del proceso del nuevo ticket inicia luego de finalizar el ticket anterior

            // en caso el ultimo ticket termine antes que el registro del ticket nuevo, la fecha de inicio del proceso parte desde el momento en que se registra el nuevo ticket y no del termino del ticket anterior.
            if (Fecha_termino_ult_ticket <= Fecha_reg_tckt_actual)
            {
                Fecha_termino_ult_ticket = Fecha_reg_tckt_actual;
            }


            int dia_semana;

            //            Friday  5
            //Indicates Friday.

            //Monday  1
            //Indicates Monday.

            //Saturday    6
            //Indicates Saturday.

            //Sunday  0
            //Indicates Sunday.

            //Thursday    4
            //Indicates Thursday.

            //Tuesday 2
            //Indicates Tuesday.

            //Wednesday   3
            //Indicates Wednesday.
            DateTime Fecha_reg_tckt_actual_final = Fecha_termino_ult_ticket;
            //dia_semana = (int)Fecha_termino_ult_ticket.DayOfWeek;
            //incio de jornada 08:30
            DateTime ini_jornada;// 8:30 del dia que se registra el ticket
            ini_jornada = Fecha_termino_ult_ticket.Date.AddDays(0);
            ini_jornada = ini_jornada.AddHours(8);
            ini_jornada = ini_jornada.AddMinutes(30);
            ini_jornada = ini_jornada.AddSeconds(0);
            //fin de jornada 19:00
            DateTime fin_jornada;// 19:00 del dia que se registra el ticket
            fin_jornada = Fecha_termino_ult_ticket.Date.AddDays(0);
            fin_jornada = fin_jornada.AddHours(19);
            fin_jornada = fin_jornada.AddMinutes(0);
            fin_jornada = fin_jornada.AddSeconds(0);

            //A partir de las 2pm (14:00) el tiempo de entrega se calcula las horas restantes hasta el fin de jornada y el resto de horas faltantes para terminar de procesar se cargan a partir del inicio de jornada del dia siguiente

            //hora de corte (2pm)
            DateTime hora_corte;
            hora_corte = Fecha_termino_ult_ticket.Date.AddDays(0);
            hora_corte = hora_corte.AddHours(14);
            hora_corte = hora_corte.AddMinutes(0);
            hora_corte = hora_corte.AddSeconds(0);

            double horas_trabajadas = 0;
            double horas_faltantes = 0;


            //CASO1:Cuando registren un ticket entre las 2pm(hora de corte) y 7 pm(fin de jornada)
            if (Fecha_termino_ult_ticket >= hora_corte && Fecha_termino_ult_ticket <= fin_jornada)
            {

                horas_trabajadas = (fin_jornada - Fecha_termino_ult_ticket).TotalHours;
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                if (horas_trabajadas <= 5)
                {
                    horas_faltantes = 5 - horas_trabajadas;

                }
                Fecha_reg_tckt_actual_final = ini_jornada.AddDays(1);

                
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(horas_faltantes);
                
                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
                
                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }

                //Fin

                goto fin;
            }
            //CASO2:Cuando registren un ticket despues de 7 pm(fin de jornada)
            if (Fecha_termino_ult_ticket > fin_jornada)
            {
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                //fecha prevista= 5 horas despues del inicio de jornada del dia siguiente del registro del ticket
                Fecha_reg_tckt_actual_final = ini_jornada.AddDays(1);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }
                //Fin
                goto fin;
            }
            //CASO3:Cuando registren un ticket antes del inicio de jornada desde las 0:00 horas
            if (Fecha_termino_ult_ticket < ini_jornada)
            {
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                //fecha prevista= 5 horas despues del inicio de jornada en el dia del registro del ticket
                Fecha_reg_tckt_actual_final = ini_jornada.AddHours(5);
                //return Fecha_reg_tckt_actual;
                //Si el ticket anterior finaliza un viernes, la fecha prevista del nuevo ticket es lunes
                dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
                if (dia_semana > 5)//sabado
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                }
                if (dia_semana == 0)//domingo
                {
                    Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                }
                //Fin
                goto fin;


            }
            //CASO4:Registro del nuevo ticket antes del fin de corte
            //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
            Fecha_reg_tckt_actual_final = Fecha_termino_ult_ticket.AddHours(5);
            dia_semana = (int)Fecha_reg_tckt_actual_final.DayOfWeek;
            if (dia_semana > 5)//sabado
            {
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays((6 - dia_semana) + 2);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.Date;
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(8);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddMinutes(30);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddSeconds(0);
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
            }
            if (dia_semana == 0)//Domingo
            {
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddDays(1);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.Date;
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(8);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddMinutes(30);
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddSeconds(0);
                //5 representa el numero de horas que demora realizar las pruebas a cualquier Libro electronico
                Fecha_reg_tckt_actual_final = Fecha_reg_tckt_actual_final.AddHours(5);
            }
            fin:
            return Fecha_reg_tckt_actual_final;
        }
    }
}