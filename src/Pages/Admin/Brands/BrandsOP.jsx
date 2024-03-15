import React, { useEffect, useState } from 'react'
import { addBrand, deleteBrand, editBrand, getBrands } from '../../../Api/Brands'
import { useToast } from '../../../store/Toast/ToastService'
import { IoAdd } from "react-icons/io5";
import BaseModal from '../../../Components/Modals/BaseModal';
import { FaRegTrashCan } from "react-icons/fa6";
import { FaRegEdit } from "react-icons/fa";


const EditBrandModal = ({Brand, handleModal, action}) =>{
    const [brand, setBrand] = useState({id:Brand?.id, name:Brand?.name})
    return(
        <BaseModal action={()=>action(Brand?.id, brand)} showModal={true} handleModal={handleModal} title={"Edit Brand"} >
            <label
                htmlFor="Brand"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Brand"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Brand Name"
                    onChange={(e)=>setBrand({...brand, name:e.target.value})}
                    value={brand?.name || Brand?.name}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Brand Name
                </span>
            </label>
        </BaseModal>

    )
}
const AddBrandModal = ({handleModal, action}) =>{
    const [Brand, setBrand] = useState("")

    return(
        <BaseModal action={()=>action(Brand)} showModal={true} handleModal={handleModal} title={"Add new Brand"} >
            <label
                htmlFor="Brand"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Brand"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Brand Name"
                    onChange={(e)=>{
                        setBrand(e.target.value)
                    }}
                    value={Brand}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Brand Name
                </span>
            </label>
        </BaseModal>

    )
}




const BrandsOP = () => {
    const [Brands, SetBrands] = useState([])
    const toast = useToast()
    const [AddModal, setAddModal] = useState(false);
    const [EditModal, setEditModal] = useState(null)
    const [Brand, setBrand] = useState(null);
    const [changed, setChanged] = useState(false)
    
    const DeleteBrand = (id) =>{
        deleteBrand(id)
            .then(res=>{
                if(res.status === 200){
                    toast.open("Success", res.data,'success')
                    setChanged(!changed)
                }else{
                    toast.open("Error", res.data,'error')
                    setShowModal(!showModal)
                }
            })
    }

    const AddNewBrand = (Brand) =>{
        addBrand({"name":Brand})
            .then(res=>{
                if(res.status === 200){
                    toast.open("Success", res.data,'success')
                    setChanged(!changed)
                }else{
                    toast.open("Error", res.data,'error')
                }
            }).catch(err=>toast.open("Error", err.message,'error'))
            setAddModal(!AddModal)
    }

    const EditBrand = (id, Brand) =>{
        Brand.id = id
        editBrand(id, Brand)
            .then(res=>{
                if(res.status === 200){
                    toast.open("Success", res.data,'success')
                    setChanged(!changed)
                }else{
                    toast.open("Error", res.data,'error')
                }
            }).catch(err=>toast.open("Error", err.message,'error'))
            setEditModal(!EditModal)

    }
    
    useEffect(()=>{
        getBrands()
            .then(res=>SetBrands(res.data))
            .catch(err=>toast('Error', err.message, 'error'))
    },[changed])

    const ShowAddModal = ()=>{
        setAddModal(!AddModal)
    } 
    const ShowEditModal = ()=>{
        setEditModal(!EditModal)
    }
  return (
    <>
        {/* <BaseModal action={AddNewBrand} showModal={showModal} handleModal={handleModal} title={"Add new Brand"} >
            <label
                htmlFor="Brand"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Brand"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Brand Name"
                    onChange={(e)=>setNewBrand(e.target.value)}
                    value={newBrand}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Brand Name
                </span>
            </label>
        </BaseModal> */}
        {
            EditModal?
                <EditBrandModal 
                    Brand={Brand} 
                    action={EditBrand} 
                    showModal={EditModal} 
                    handleModal={ShowEditModal} 
                    title={"Edit Brand"} 
                />
            :null
        }
        {
            AddModal?
                <AddBrandModal 
                    action={AddNewBrand}
                    showModal={AddModal} 
                    handleModal={ShowAddModal} 
                    title={"Add new Brand"} 
                />
            :null
        }
        

        
        

        
        <div className='p-4 w-full'>
            <button
                className="inline-flex items-center gap-2 rounded border border-indigo-600 bg-indigo-600 px-8 py-3 text-white hover:bg-transparent hover:text-indigo-600 focus:outline-none focus:ring active:text-indigo-500"
                onClick={()=>ShowAddModal()}
            >
                <IoAdd />
                <span className="text-sm font-medium"> Add New </span>

            </button>
            

            <div className="py-3 px-5 default-shadow rounded-md my-3">
            {
                Brands.length > 0?
                    <div className="flow-root w-50">
                        {
                        Brands.map(b=>
                            <dl className=" divide-y divide-gray-100 text-sm" key={b.id}>
                                <div className="grid grid-cols-1 gap-1 py-3 sm:grid-cols-3 sm:gap-4">
                                    <dt className="font-medium text-gray-900">{b.name}</dt>
                                    <dd className="text-gray-700 sm:col-span-2">
                                        <div className="inline-flex gap-3 rounded-lg border border-gray-100 bg-gray-100 p-1">
                                            <button
                                                onClick={()=>{
                                                    setBrand(b)
                                                    ShowEditModal()
                                                }}
                                                className="inline-flex items-center gap-2 rounded-md bg-white px-4 py-2 text-sm text-blue-500 shadow-sm focus:relative"
                                            >
                                                <FaRegEdit />
                                                


                                                Edit
                                            </button>

                                            <button
                                                onClick={()=>DeleteBrand(b.id)}
                                                className="inline-flex items-center gap-2 rounded-md bg-white px-4 py-2 text-sm text-red-500 shadow-sm focus:relative"
                                            >
                                                <FaRegTrashCan />
                                                Delete
                                            </button>
                                        </div>
                                    </dd>
                                </div>
                            </dl>
                            )
                        }
                    </div>
                :null
            }
            </div>
        </div>
    </>
  )
}

export default BrandsOP
