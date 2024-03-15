import axiosClient from "./axiosClient"

export const getBrandsAsList = () => 
    axiosClient.get('/api/Brands/as-select')


export const getBrands = () => 
    axiosClient.get('/api/Brands')

export const addBrand = (data) =>
    axiosClient.post('/api/Brands/add', data)

export const editBrand = (id, data) =>
    axiosClient.put('/api/Brands/edit/'+id, data)

    

export const deleteBrand = (id) =>
    axiosClient.delete('/api/Brands/delete/'+id)