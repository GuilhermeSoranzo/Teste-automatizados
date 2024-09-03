using CursoOnlineTeste._Builders;
using CursoOnlineTeste._Util;
using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CursoOnlineTeste.Cursos
{
    public class CursoTeste : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHorario;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public void Dispose()
        {
            _output.WriteLine("Executando Disposable...");
        }

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Executando Construtor...");

            _nome = "Informática";
            _descricao = "Informática sei la";
            _cargaHorario = 80;
            _publicoAlvo = PublicoAlvo.Estudadente;
            _valor = 950;
        }

        [Fact]
        public void CriaCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHorario = _cargaHorario,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(_nome, _descricao, _cargaHorario, _publicoAlvo, _valor);

            //cursoEsperado.ToExpectedObject().ShouldMatch(curso);
            Assert.Equivalent(cursoEsperado, curso);
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeVazio()
        {

            Assert.Throws<ArgumentException>(() => new Curso(string.Empty, _descricao, _cargaHorario, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        [Fact]
        public void NaoDeveCursoTerUmNomeNull()
        {
            Assert.Throws<ArgumentException>(() => new Curso(null, _descricao, _cargaHorario, _publicoAlvo, _valor)).ComMensagem("Nome inválido");
        }

        //Usando o theory para agrupar os métodos NaoDeveCursoTerUmNomeNull & NaoDeveCursoTerUmNomeVazio na mesma função de teste
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ArgumentException>(() => 
                CursoBuilder.Novo().ComNome(nomeInvalido).Builder())
                .ComMensagem("Nome inválido");
        }

        [Fact]
        public void NaoDeveCursoTerUmaCargaHorarioMenorQue1()
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargahoraria(0).Builder())
                .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorarioMenorOuIgualAZero(double cargaHorarioInvalid)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargahoraria(cargaHorarioInvalid).Builder())
                .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalid)
        {
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalid).Builder())
                .ComMensagem("Valor inválido");
        }        
    }

    public class Curso
    {
        public Curso(string nome, string descricao,double cargaHorario, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome)) throw new ArgumentException("Nome inválido");

            if(cargaHorario < 1) throw new ArgumentException("Carga horária inválida");

            if(valor < 1) throw new ArgumentException("Valor inválido");

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHorario = cargaHorario;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHorario { get; set; }
        public PublicoAlvo PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }

    public enum PublicoAlvo 
    {
        Estudadente,
        Universitario,
        Empregado,
        Empreendedor
    }
}
