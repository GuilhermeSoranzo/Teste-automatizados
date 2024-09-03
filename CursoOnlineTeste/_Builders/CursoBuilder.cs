using CursoOnlineTeste.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CursoOnlineTeste._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática";
        private string _descricao = "Informática sei la";
        private double _cargaHorario = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudadente;
        private double _valor = 950;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargahoraria(double cargaHorario)
        {
            _cargaHorario = cargaHorario;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }
        
        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public Curso Builder()
        {
            return new Curso(_nome, _descricao, _cargaHorario, _publicoAlvo, _valor);
        }
    }
}
