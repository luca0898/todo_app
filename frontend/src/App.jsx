import axios from "axios";
import { useEffect, useState } from "react";
import { getPagged, create, update, remove } from "./Services"
import styles from "./app.module.scss"

function App() {
  const [newTodo, setNewTodo] = useState("")
  const [todos, setTodos] = useState([])

  useEffect(() => {
    getAll()
  }, [])

  async function getAll() {
    const { data: responseBody } = await getPagged(1, 20)

    setTodos(responseBody.data)
  }

  async function createNewTodo() {
    if (!newTodo) {
      return;
    }

    const { data: responseBody } = await create({
      title: newTodo,
      finished: false
    })

    setTodos(o => [...o, responseBody.data])
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
          {todos?.map((todo, index) => (
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
