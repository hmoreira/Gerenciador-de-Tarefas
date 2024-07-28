using TaskManager.Core.Entities;
using TaskManager.Core.Enums;
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
        public void UsuarioCriadorPreenchido_Success()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1, 1);
            var result = ModelValidator.ValidateModel(model);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void UsuarioCriadorPreenchido_Fail()
        {
            var model = new Tarefa("Task 1", "Task 1 xxx", DateTime.Now.AddDays(7),
                                   TarefaStatusEnum.Pendente, 1,0);
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
    }
}