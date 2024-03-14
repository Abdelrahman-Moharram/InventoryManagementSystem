import axiosClient from "./axiosClient"

export const getAllProducts = () => 
    axiosClient.get('/api/Products/table')

export const getAllProductswithIncludes = () => 
    axiosClient.get('/api/Products/all')

export const getProductById = (id) => 
    axiosClient.get(`/api/Products/${id}`)

export const getProductByIdforForm = (id) => 
    axiosClient.get(`/api/Products/form/${id}`)

export const getProductByCategoryNameOrBrandName = (categoryName=null, brandName=null) => 
    axiosClient.get(`/api/Products/search`, {params:{categoryName, brandName}})


export const AddProduct = (data) =>
    axiosClient.post(`/api/Products/add`, data)


export const UpdateProduct = (id, data) =>
    axiosClient.put(`/api/Products/edit/${id}`, data)


export const DeleteProduct = (id) =>{
    console.log(id);
    return axiosClient.delete(`/api/Products/delete/${id}`)
}