import { useEffect, useState } from "react";
import styles from "./app.module.scss";
import { create, getPagged, remove, update } from "./Services";

function App() {
  const [newTodo, setNewTodo] = useState("")
  const [todos, setTodos] = useState([])

  useEffect(() => {
    getAll()
  }, [])

  async function getAll() {
    const { data: { data: responseBody } } = await getPagged(1, 20)

    setTodos(responseBody)
  }

  async function createNewTodo() {
    if (!newTodo)
      return;

    const { data: { data: responseBody } } = await create({
      title: newTodo,
      finished: false
    })

    setTodos(o => [...o, responseBody])
    setNewTodo("")
  }

  async function toggleTodo(todo) {
    const modifiedTodo = { ...todo, finished: !todo.finished };

    await update(todo.id, modifiedTodo)

    setTodos(o => o.map(m => m.id === todo.id ? modifiedTodo : m))
  }


  async function removeTodo(id) {
    await remove(id)

    setTodos(oldState => oldState.filter(todo => todo.id !== id))
  }

  return (
    <div>

      <header className={styles.controls}>
        <input
          type="text"
          placeholder="Digite uma descrição ..."
          value={newTodo}
          onChange={(value) => setNewTodo(value.target.value)}
        />
        <button className={styles.add_button} onClick={createNewTodo}>+</button>
      </header>

      <div>
        <ul>
          {todos?.map((todo) => (
            <li key={todo.id} className={styles.todo_item}>
              <div>
                <label className={todo.finished ? styles.todo_finished : ""}>{todo.title}</label>
                <button onClick={() => toggleTodo(todo)}>{todo.finished ? "Reativar" : "Finalizar"}</button>
                <button onClick={() => removeTodo(todo.id)}>Remover</button>
              </div>
            </li>
          ))}
        </ul>
      </div>

    </div>
  );
}

export default App;
