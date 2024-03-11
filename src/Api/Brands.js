import axiosClient from "./axiosClient"

export const getBrandsAsList = () => 
    axiosClient.get('/api/Brands/as-select')