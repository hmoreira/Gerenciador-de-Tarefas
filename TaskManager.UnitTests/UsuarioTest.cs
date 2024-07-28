using TaskManager.Core.Enums;
using TaskManager.Core.Utils;
using TaskManager.Core.Entities;

namespace TaskManager.UnitTests
{
    public class UsuarioTest
    {
        Usuario usuario { get; } = new Usuario("Heber", UserFuncaoEnum.Regular, "heber", "970803");
        [Fact]
        public void NomePreenchido_Success()
        {            
            var result = ModelValidator.ValidateModel(usuario);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void NomePreenchido_Fail()
        {
            var model = new Usuario("", UserFuncaoEnum.Regular, "heber", "970803");
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }
        [Fact]
        public void SenhaPreenchida_Success()
        {            
            var result = ModelValidator.ValidateModel(usuario);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void SenhaPreenchida_Fail()
        {
            var model = new Usuario("Heber", UserFuncaoEnum.Regular, "heber", "");
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }
        [Fact]
        public void UsernamePreenchido_Success()
        {            
            var result = ModelValidator.ValidateModel(usuario);
            Assert.True(result.Count == 0);
        }
        [Fact]
        public void UsernamePreenchido_Fail()
        {
            var model = new Usuario("Heber", UserFuncaoEnum.Regular, "", "970803");
            var result = ModelValidator.ValidateModel(model);
            Assert.False(result.Count == 0);
        }
    }
}