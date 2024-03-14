import React from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { AddProduct, UpdateProduct, getProductByIdforForm} from '../../../Api/Products'
import { useToast } from '../../../store/Toast/ToastService'
import ProductForm from '../../../Components/Forms/ProductForm'

const EditProduct = () => {
    const nav = useNavigate()
    const toast = useToast()
    const {id} = useParams()

    const FormHandler = (e, product, files)=>{
        e.preventDefault()
        let form = new FormData()
        form.append('name', product.name)
        form.append('modelName', product.modelName)
        form.append('description', product.description)
        form.append('price', product.price)
        form.append('categoryId', product.categoryId)
        form.append('brandId', product.brandId)
        form.append('images', files)

        if(id)
        {
            form.append('id', product.id)   
            getProductByIdforForm(id, form)
                .then(res=>{
                    if(res.status === 200){
                        toast.open("Success", res.data,'success')
                        nav("/admin/products")
                    }else{
                        toast.open("Error", res.data,'error')
                    }
                })
        }else{
            AddProduct(form)
                .then(res=>{
                    if(res.status === 200){
                        toast.open("Success", res.data,'success')
                        nav("/admin/products")
                    }else{
                        toast.open("Error", res.data,'error')
                    }
                })
        }
    }

  return (
    <div className='p-5 w-full '>
        <div className="mt-10 w-full gap-x-6 gap-y-8 default-shadow rounded-lg p-5">
            <ProductForm FormHandler={FormHandler} id={id} />
        </div>

    </div>
  )
}

export default EditProduct
