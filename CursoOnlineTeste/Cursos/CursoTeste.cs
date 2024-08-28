using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnlineTeste.Cursos
{
    public class CursoTeste
    {
        [Fact]
        public void CriaCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHorario, cursoEsperado.PublicoAlvo, 500);

            //cursoEsperado.ToExpectedObject().ShouldMatch(curso);
            Assert.Equivalent(cursoEsperado, curso);
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeVazio()
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(string.Empty, cursoEsperado.CargaHorario, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Nome inválido", message);
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeNull()
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(null, cursoEsperado.CargaHorario, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Nome inválido", message);
        }

        //Usando o theory para agrupar os métodos NaoDeveCursoTerUmNomeNull & NaoDeveCursoTerUmNomeVazio na mesma função de teste
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(nomeInvalido, cursoEsperado.CargaHorario, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Nome inválido", message);
        }

        [Fact]
        public void NaoDeveCursoTerUmaCargaHorarioMenorQue1()
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, 0, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Carga horária inválida", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorarioMenorOuIgualAZero(double cargaHorarioInvalid)
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cargaHorarioInvalid, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)).Message;
            Assert.Equal("Carga horária inválida", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalid)
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHorario = 80,
                PublicoAlvo = PublicoAlvo.Estudadente,
                Valor = 500

            };

            var message = Assert.Throws<ArgumentException>(() => new Curso(cursoEsperado.Nome, cursoEsperado.CargaHorario, cursoEsperado.PublicoAlvo, valorInvalid)).Message;
            Assert.Equal("Valor inválido", message);
        }
    }

    public class Curso
    {
        public string Nome { get; set; }
        public double CargaHorario { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public Curso(string nome, double cargaHorario, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome inválido");

            if(cargaHorario < 1) throw new ArgumentException("Carga horária inválida");

            if(valor < 1) throw new ArgumentException("Valor inválido");

            this.Nome = nome;
            this.CargaHorario = cargaHorario;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }
    }

    public enum PublicoAlvo 
    {
        Estudadente,
        Universitario,
        Empregado,
        Empreendedor
    }
}
