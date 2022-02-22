import axios from "axios";
import { useEffect, useState } from "react";
import styles from "./app.module.scss"

function App() {
  const [newTodo, setNewTodo] = useState("")
  const [todos, setTodos] = useState([])

  useEffect(() => {
    axios.get('https://localhost:4000/todo')
      .then(response => response.data)
      .then((data) => setTodos(data.data))
      .catch(console.error)
  }, [])

  function createNewTodo() {
    if (!newTodo) {
      return;
    }

    axios.post('https://localhost:4000/todo', {
      title: newTodo,
      finished: false
    })
      .then(response => response.data)
      .then((data) => {
        setTodos(o => [...o, data])
        setNewTodo("")
      })
      .catch(console.error)
  }

  function toggleTodo(todo) {
    const modifiedTodo = { ...todo, finished: !todo.finished };

    axios.put(`https://localhost:4000/todo/${todo.id}`, modifiedTodo)
      .then(response => setTodos(o => o.map(m => m.id === todo.id ? modifiedTodo : m)))
      .catch(console.error)
  }


  function removeTodo(id) {
    axios.delete(`https://localhost:4000/todo/${id}`)
      .then(() => setTodos(o => o.filter(f => f.id !== id)))
      .catch(console.error)
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
