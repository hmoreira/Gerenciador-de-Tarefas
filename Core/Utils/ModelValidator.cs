
using System.ComponentModel.DataAnnotations;
using TaskManager.Core.Entities;


namespace TaskManager.Core.Utils
{
    public class ModelValidator 
    {
        public static IList<ValidationResult> ValidateModel(object model, bool isUpdate = false)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(model, validationContext, results, true);

            if (model is IValidatableObject validatableModel)
                results.AddRange(validatableModel.Validate(validationContext));            

            if (model.GetType() == typeof(Usuario))
            {
                var user = (Usuario)model;
                if (string.IsNullOrEmpty(user.Senha) && !isUpdate)
                    results.Add(new ValidationResult("Senha obrigatoria pra criacao de usuario"));
                if (string.IsNullOrEmpty(user.Username) && !isUpdate)
                    results.Add(new ValidationResult("Username obrigatorio pra criacao de usuario"));
            }
            else //tarefa
            {
                var tarefa = (Tarefa)model;
                if (tarefa.UsuarioCriadorId == 0 && isUpdate)
                    results.Add(new ValidationResult("Usuario criador da tarefaobrigatoria pra criacao de usuario"));
            }

            return results;
        }        
    }
}
