import axiosClient from "./axiosClient"

export const getAllProducts = () => 
    axiosClient.get('/api/Products/table')

export const getAllProductswithIncludes = () => 
    axiosClient.get('/api/Products/all')
    
export const getProductById = (id) => 
    axiosClient.get(`/api/Products/${id}`)

export const getProductByCategoryNameOrBrandName = (categoryName=null, brandName=null) => 
    axiosClient.get(`/api/Products/search`, {params:{categoryName, brandName}})