import React, { useEffect, useState } from 'react'
import { getCategoriesAsList } from '../../../Api/Categories'
import { getBrandsAsList } from '../../../Api/Brands'

const EditProduct = () => {
    useEffect(()=>{
        getCategoriesAsList()
            .then(data=>{
                setCategories(()=>data.data)
            })
        .catch(err=>console.log(err))

        
    })

    useEffect(()=>{
        getBrandsAsList()
            .then(data=>{
                setBrands(()=>data.data)
            })
        .catch(err=>console.log(err))
    })
    
    const [product, setProduct] = useState({
        name:"",
        modelName:"",
        description:"",
        price:"",
        categoryId:"",
        brandId:""
    })
    const [categories, setCategories] = useState([])
    const [brands, setBrands] = useState([])

    const FormHandler = (e)=>{
        console.log(product);
        e.preventDefault()
    }

  return (
    <div className='p-5 w-full '>


    <div className="mt-10 w-full gap-x-6 gap-y-8 default-shadow rounded-lg p-5">
      <form className='grid grid-cols-2 content-evenly' method="post" onSubmit={FormHandler}>
        <div>
            <div className="mt-2">
                <label htmlFor="Name" className="block text-sm font-medium leading-6 text-gray-900">
                Name
                </label>
                <input
                    defaultValue={product.name}
                    type="text"
                    name="Name"
                    id="Name"
                    onChange={e=>setProduct(prev => ({...prev, name: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
            </div>
            <div className="mt-2">
                <label htmlFor="ModelName" className="block text-sm font-medium leading-6 text-gray-900">
                ModelName
                </label>
                <input
                    defaultValue={product.modelName}
                    type="text"
                    name="ModelName"
                    id="ModelName"
                    onChange={e=>setProduct(prev => ({...prev, modelName: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
            </div>
            <div className="mt-2">
                <label htmlFor="Description" className="block text-sm font-medium leading-6 text-gray-900">
                Description
                </label>
                <textarea
                    defaultValue={product.description}
                    type="text"
                    name="Description"
                    id="Description"
                    rows={4}
                    onChange={e=>setProduct(prev => ({...prev, description: e.target.value}))}
                    className="block resize-none w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                ></textarea>
            </div>

            <div className="mt-2">
                <label htmlFor="price" className="block text-sm font-medium leading-6 text-gray-900">
                price
                </label>
                <input
                    defaultValue={product.price}
                    type="text"
                    name="price"
                    id="price"
                    onChange={e=>setProduct(prev => ({...prev, price: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
            </div>

            <div className="mt-2">
                <label htmlFor="price" className="block text-sm font-medium leading-6 text-gray-900">
                Category
                </label>
                <select
                    defaultValue={product.categoryId}
                    type="text"
                    name="categoryId"
                    id="categoryId"
                    onChange={e=>setProduct(prev => ({...prev, categoryId: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                >
                    
                    <option value=""></option>
                    {
                        categories.map(cat=><option key={cat.id} value={cat.id}>{cat.name}</option>)
                    }
                </select>
            </div>

            <div className="mt-2">
                <label htmlFor="price" className="block text-sm font-medium leading-6 text-gray-900">
                Brand
                </label>
                <select
                    defaultValue={product.brandId}
                    type="text"
                    name="brandId"
                    id="brandId"
                    onChange={e=>setProduct(prev => ({...prev, brandId: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                >
                    <option value=""></option>
                    {
                        brands.map(brand=><option key={brand.id} value={brand.id}>{brand.name}</option>)
                    }
                </select>
            </div>
        </div>


        <div></div>
        
        <button type="submit" className='bg-[black] text-white'>submit</button>
      </form>
    </div>

    </div>
  )
}

export default EditProduct
