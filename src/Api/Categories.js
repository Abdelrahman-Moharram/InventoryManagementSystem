import axiosClient from "./axiosClient"

export const getCategoriesAsList = () => 
    axiosClient.get('/api/Categories/as-select')