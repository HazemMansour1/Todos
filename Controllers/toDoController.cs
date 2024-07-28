using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Todos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class toDoController : ControllerBase
    {
        private static List<toDo> toDos = new List<toDo>();
        private static int num = 1;
        [HttpGet("getAllTasks")]
        public IActionResult getAllTask()
        {
            return Ok(toDos);
        }
        [HttpGet("getTaskByNum")]

        public IActionResult getTaskByNum(int num)
        {
            var tsk = toDos.SingleOrDefault(t => t.taskNum == num);
            if (tsk == null)
            {
                return NotFound();
            }
            return Ok(tsk);
        }

        [HttpPost("addTask")]
        public IActionResult addTask(toDo tsk)
        {
            tsk.taskNum = num++;
            toDos.Add(tsk);
            return CreatedAtAction(nameof(getTaskByNum),new {num = tsk.taskNum},tsk);
        }
        [HttpDelete("deleteTask")]

        public IActionResult deleteTask(int n)
        {
            var tsk = toDos.SingleOrDefault(t => t.taskNum == n);
            if (tsk == null)
            {
                return NotFound();
            }
            toDos.Remove(tsk);
            return NoContent();
        }
        [HttpPut("updateTask")]
        public IActionResult UpdateEmployee(toDo updatedTask)
        {
            var tsk = toDos.SingleOrDefault(t => t.taskNum == updatedTask.taskNum);
            if (tsk == null)
            {
                return NotFound();
            }

            tsk.taskName = updatedTask.taskName;
            tsk.taskDue = updatedTask.taskDue;
            tsk.done = updatedTask.done;
            return Ok(tsk);

        }
    }
}
