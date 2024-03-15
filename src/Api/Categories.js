import axiosClient from "./axiosClient"

export const getCategoriesAsList = () => 
    axiosClient.get('/api/Categories/as-select')

export const getCategories = () => 
    axiosClient.get('/api/Categories')

export const addCategory = (data) =>
    axiosClient.post('/api/Categories/add', data)

export const editCategory = (id, data) =>
    axiosClient.put('/api/Categories/edit/'+id, data)

    

export const deleteCategory = (id) =>
    axiosClient.delete('/api/Categories/delete/'+id)