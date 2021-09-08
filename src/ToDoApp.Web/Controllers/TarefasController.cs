using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces.Repositories;

namespace ToDoApp.Web.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITarefaService _tarefaService;

        public TarefasController(ITarefaRepository tarefaRepository,
                                 ICategoriaRepository categoriaRepository,
                                 ITarefaService tarefaService)
        {
            _tarefaRepository = tarefaRepository;
            _categoriaRepository = categoriaRepository;
            _tarefaService = tarefaService;
        }

        public async Task<IActionResult> Index()
        {
            var tarefas = await _tarefaRepository.ObterTodos();
            return View(tarefas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tarefa = await _tarefaRepository.ObterPorId(id.Value);
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        public async Task<IActionResult> Create()
        {
            var categorias = await _categoriaRepository.ObterTodos();
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                var sucesso = await _tarefaService.CriarTarefa(tarefa);
                if (sucesso)
                    return RedirectToAction(nameof(Index));
            }
            return await Create();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var categorias = await _categoriaRepository.ObterTodos();
            ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nome");

            var tarefa = await _tarefaRepository.ObterPorId(id.Value);
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
        {
            if (id != tarefa.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var sucesso = await _tarefaService.AtualizarTarefa(tarefa);
                if (sucesso)
                    return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tarefa = await _tarefaRepository.ObterPorId(id.Value);
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _tarefaRepository.ObterPorId(id);
            var sucesso = await _tarefaService.RemoverTarefa(tarefa);
            
            if (sucesso)
                return RedirectToAction(nameof(Index));

            return await Delete(id);
        }

        public async Task<IActionResult> Concluir(int id)
        {
            var tarefa = await _tarefaRepository.ObterPorId(id);
            var sucesso = await _tarefaService.ConcluirTarefa(tarefa);

            return RedirectToAction(nameof(Index));
        }
    }
}
