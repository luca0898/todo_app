import axios from "axios"

export const todoApi = axios.create({
    baseURL: `${process.env.REACT_APP_TODO_API_URL}/todo`,
    timeout: 1000
})

export const getPagged = async (currentPage, pageSize) => (
    await todoApi.get("", {
        params: {
            currentPage,
            pageSize
        }
    })
)

export const create = async model => await todoApi.post("", model)

export const update = async (id, model) => await todoApi.put(`${id}`, model)

export const remove = async (id) => await todoApi.delete(`${id}`)