import React, { useEffect, useState } from 'react'
import { addCategory, deleteCategory, editCategory, getCategories } from '../../../Api/Categories'
import { useToast } from '../../../store/Toast/ToastService'
import { IoAdd } from "react-icons/io5";
import BaseModal from '../../../Components/Modals/BaseModal';
import { FaRegTrashCan } from "react-icons/fa6";
import { FaRegEdit } from "react-icons/fa";


const EditCategoryModal = ({Category, handleModal, action}) =>{
    const [category, setCategory] = useState({id:Category?.id, name:Category?.name})
    return(
        <BaseModal action={()=>action(Category?.id, category)} showModal={true} handleModal={handleModal} title={"Edit Category"} >
            <label
                htmlFor="Category"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Category"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Category Name"
                    onChange={(e)=>setCategory({...category, name:e.target.value})}
                    value={category?.name || Category?.name}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Category Name
                </span>
            </label>
        </BaseModal>

    )
}
const AddCategoryModal = ({handleModal, action}) =>{
    const [category, setCategory] = useState("")

    return(
        <BaseModal action={()=>action(category)} showModal={true} handleModal={handleModal} title={"Add new Category"} >
            <label
                htmlFor="Category"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Category"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Category Name"
                    onChange={(e)=>{
                        setCategory(e.target.value)
                    }}
                    value={category}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Category Name
                </span>
            </label>
        </BaseModal>

    )
}




const CategoriesOP = () => {
    const [Categories, SetCategories] = useState([])
    const toast = useToast()
    const [AddModal, setAddModal] = useState(false);
    const [EditModal, setEditModal] = useState(null)
    const [Category, setCategory] = useState(null);
    const [changed, setChanged] = useState(false)
    
    const DeleteCategory = (id) =>{
        deleteCategory(id)
            .then(res=>{
                if(res.status === 200){
                    toast.open("Success", res.data,'success')
                    SetCategories(cats=>cats.filter(c=>c.name !== id))
                    setChanged(!changed)
                }else{
                    toast.open("Error", res.data,'error')
                    setShowModal(!showModal)
                }
            })
    }

    const AddNewCategory = (category) =>{
        addCategory({"name":category})
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

    const EditCategory = (id, category) =>{
        category.id = id
        editCategory(id, category)
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
        getCategories()
            .then(res=>SetCategories(res.data))
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
        {/* <BaseModal action={AddNewCategory} showModal={showModal} handleModal={handleModal} title={"Add new Category"} >
            <label
                htmlFor="Category"
                className="relative block w-[400px] rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    autoComplete="off"
                    type="text"
                    id="Category"
                    className="peer border-none bg-transparent placeholder-transparent focus:border-transparent focus:outline-none focus:ring-0 p-3"
                    placeholder="Category Name"
                    onChange={(e)=>setNewCategory(e.target.value)}
                    value={newCategory}
                />
                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Category Name
                </span>
            </label>
        </BaseModal> */}
        {
            EditModal?
                <EditCategoryModal 
                    Category={Category} 
                    action={EditCategory} 
                    showModal={EditModal} 
                    handleModal={ShowEditModal} 
                    title={"Edit Category"} 
                />
            :null
        }
        {
            AddModal?
                <AddCategoryModal 
                    action={AddNewCategory}
                    showModal={AddModal} 
                    handleModal={ShowAddModal} 
                    title={"Add new Category"} 
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
                Categories.length > 0?
                    <div className="flow-root w-50">
                        {
                        Categories.map(cat=>
                            <dl className=" divide-y divide-gray-100 text-sm" key={cat.id}>
                                <div className="grid grid-cols-1 gap-1 py-3 sm:grid-cols-3 sm:gap-4">
                                    <dt className="font-medium text-gray-900">{cat.name}</dt>
                                    <dd className="text-gray-700 sm:col-span-2">
                                        <div className="inline-flex gap-3 rounded-lg border border-gray-100 bg-gray-100 p-1">
                                            <button
                                                onClick={()=>{
                                                    setCategory(cat)
                                                    ShowEditModal()
                                                }}
                                                className="inline-flex items-center gap-2 rounded-md bg-white px-4 py-2 text-sm text-blue-500 shadow-sm focus:relative"
                                            >
                                                <FaRegEdit />
                                                


                                                Edit
                                            </button>

                                            <button
                                                onClick={()=>DeleteCategory(cat.id)}
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

export default CategoriesOP
