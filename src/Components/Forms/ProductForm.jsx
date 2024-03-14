import React, { useEffect, useState  } from 'react'
import { getProductById } from '../../Api/Products'
import { getCategoriesAsList } from '../../Api/Categories'
import { getBrandsAsList } from '../../Api/Brands'
import { IoMdCloseCircleOutline } from "react-icons/io";

const ProductForm = ({FormHandler, id}) => {


    const [files, setFile] = useState([]);

    const [product, setProduct] = useState({
        id: id,
        name:"",
        modelName:"",
        description:"",
        price:"",
        categoryId:"",
        brandId:""
    })


    
    useEffect(()=>{
        getBrandsAsList()
            .then(res=>{
                setBrands(()=>res.data)
            })
        .catch(err=>console.log(err))
    },[])
    
    useEffect(()=>{
        getCategoriesAsList()
            .then(res=>{
                setCategories(()=>res.data)
            })
        .catch(err=>console.log(err))

        
    },[])

    useEffect(()=>{
        if(id)
            getProductById(id)
                .then(res=>{
                    setProduct(()=>res.data)
                    setFile(res.data.productImages?.map(im=>process.env.SERVER_URL + im))
                })
            .catch(err=>console.log(err))
    },[id])
    const removeImage = (i) => {
        setFile(files.filter(x => x.name !== i));
    }

    const [categories, setCategories] = useState([])
    const [brands, setBrands] = useState([])
    const [message, setMessage] = useState();
    const handleFile = (e) => {
        setMessage("");
        let file = e.target.files;

        for (let i = 0; i < file.length; i++) {
            const fileType = file[i]['type'];
            const validImageTypes = ['image/gif', 'image/jpeg', 'image/png'];
            if (validImageTypes.includes(fileType)) {
                setFile([...files, file[i]]);
            } else {
                setMessage("only images accepted");
            }

        }
    }; 
  return (
    <form className='grid grid-cols-2 content-evenly' method="post" onSubmit={(e)=>FormHandler(e, product, files)}>
        
        <div>
            <div className="flex justify-center h-full items-center ">
                <div className="rounded-lg shadow-xl bg-gray-50 md:w-1/2 w-[360px]">
                    <div className="m-4">
                        <span className="flex justify-center items-center text-[12px] mb-1 text-red-500">{message}</span>
                        <div className="flex items-center justify-center w-full">
                            <label className="flex cursor-pointer flex-col w-full h-32 border-2 rounded-md border-dashed hover:bg-gray-100 hover:border-gray-300">
                                <div className="flex flex-col items-center justify-center pt-7">
                                    <svg xmlns="http://www.w3.org/2000/svg"
                                        className="w-12 h-12 text-gray-400 group-hover:text-gray-600" viewBox="0 0 20 20"
                                        fill="currentColor">
                                        <path fillRule="evenodd"
                                            d="M4 3a2 2 0 00-2 2v10a2 2 0 002 2h12a2 2 0 002-2V5a2 2 0 00-2-2H4zm12 12H4l4-8 3 6 2-4 3 6z"
                                            clipRule="evenodd" />
                                    </svg>
                                    <p className="pt-1 text-sm tracking-wider text-gray-400 group-hover:text-gray-600">
                                        Select a photo</p>
                                </div>
                                <input type="file" onChange={handleFile} className="opacity-0" multiple="multiple" name="files[]" />
                            </label>
                        </div>
                        <div className="flex flex-wrap gap-2 mt-2">

                            {
                                files?
                                files?.map((file, key) => {
                                    return (
                                        <div key={key} className="overflow-hidden relative">
                                        <i onClick={() => { removeImage(file.name) }} className="mdi mdi-close absolute right-1 hover:text-white cursor-pointer"></i>
                                        <img className="h-20 w-20 rounded-md" src={file} />
                                    </div>
                                )
                            })
                            :null
                        }



                        </div>
                    </div>
                </div>
            </div>
        </div>
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
                    value={product.categoryId}
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
                    value={product.brandId}
                    type="text"
                    name="brandId"
                    id="brandId"
                    onChange={e=>setProduct(prev => ({...prev, brandId: e.target.value}))}
                    className="block w-full px-2 rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                >
                    <option value=""></option>
                    {
                        brands?
                            brands.map(brand=><option key={brand.id} value={brand.id}>{brand.name}</option>)
                        :null
                    }
                </select>
            </div>
            <div className='flex justify-end mt-5'>
                <button
                    type="submit"
                    className="inline-block rounded border border-indigo-600 bg-indigo-600 px-8 py-3 text-sm font-medium text-white hover:bg-transparent hover:text-indigo-600 focus:outline-none focus:ring active:text-indigo-500"
                >
                    Save
                </button>
            </div>
        </div>


        
        
    </form>
  )
}

export default ProductForm