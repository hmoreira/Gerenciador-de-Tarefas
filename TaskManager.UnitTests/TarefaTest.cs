using TaskManager.Core.Entities;
using TaskManager.Core.Enums;
using TaskManager.Core.Services.Tarefa;
using TaskManager.Core.Services.Usuario;
using TaskManager.Core.Utils;

namespace TaskManager.UnitTests
{
    public class TarefaTest
    {
        [Fact]        
        public void TituloPreenchido_Success()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7), 
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void TituloPreenchido_Fail()
        {
            var model = new Tarefa("", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }
        [Fact]
        public void DescricaoPreenchida_Success()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void DescricaoPreenchida_Fail()
        {
            var model = new Tarefa("Task 1", "", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }        
        [Fact]
        public void UsuarioResponsavelPreenchido_Success()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void UsuarioResponsavelPreenchido_Fail()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 0,1);
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }
        [Fact]
        public void ValidaAlteracaoRemocaoTarefasOutrosUsuarios_Success()
        {
            int usuarioLogado = 1; //usuario igual criador da tarefa

            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ValidaPutDelete.Validacao(usuarioLogado, UserFuncaoEnum.Regular, model.UsuarioCriadorId);
            Assert.True(result);
        }
        [Fact]
        public void ValidaAlteracaoRemocaoTarefasOutrosUsuarios_Fail()
        {
            int usuarioLogado = 2; //usuario diferente do criador da tarefa

            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ValidaPutDelete.Validacao(usuarioLogado, UserFuncaoEnum.Regular, model.UsuarioCriadorId);
            Assert.False(result);
        }
        [Fact]
        public void ValidaAlteracaoRemocaoTarefasOutrosUsuarios_Admin_Success()
        {
            int usuarioLogado = 2; //usuario diferente do criador da tarefa mas admin

            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ValidaPutDelete.Validacao(usuarioLogado, UserFuncaoEnum.Admin, model.UsuarioCriadorId);
            Assert.True(result);
        }
    }
}