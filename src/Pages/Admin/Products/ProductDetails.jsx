import React, { useEffect, useState } from 'react'
import { BiShoppingBag } from 'react-icons/bi'
import ImageSekelton from '../../../Components/Sekeltons/ImageSekelton'
import { getProductById } from '../../../Api/Products'
import { useParams } from 'react-router-dom'
import ProductInfo from '../../../Components/Lists/ProductsInfo'
import DataTable from '../../../Components/Tables/DataTable'

const ProductDetails = () => {
    const [product, setProduct] = useState({})
    const params = useParams()
    useEffect(()=>{
      getProductById(params.id).then(res=>{
        setProduct(res.data)
      })
    },[params.productId])

    return (
<div className='container-fluid px-4 w-full'>
 
        {/* <div className='px-10 md:px-28 py-8'>
          <BreadCrumb  sections={path.split("/").slice(2)} />
        </div> */}
      
    <div className="grid grid-cols-1 sm:grid-cols-2 mt-10 gap-5 place-items-center mb-5">
        {/* image */}
        <div className='shadow-lg shadow-neutral-400 '>
            {
            product?.images?
                <img 
                    src={`http://localhost:5241${product?.images[0]}`}

                    alt='banner'
                    sizes="100vw"
                    style={{ width: '400px', height: 'auto' }}
                />
                :
                <ImageSekelton width={"400px"} height={"225px"} />
            }
        </div>
        <ProductInfo product={product} />
        

    </div>

    {/* 
        ------------------------------------------------------------------
        Inventories 
        ------------------------------------------------------------------
    */}
    {
        product?.productsInventory
        ?
        <>
            <hr className='gap-5 my-5 ' />
            <h2 className='text-xl font-bold'>Inventories</h2>
            <DataTable data={product.productsInventory} keys={["name"]} keyfilter={["name"]} options={false} />
        </>
    :null
    }

    {/* 
        ------------------------------------------------------------------
        Items 
        ------------------------------------------------------------------
    */}
    {
        product?.productItems
        ?
        
            <>
            <hr className='gap-5 my-5 ' />
            <h2 className='text-xl font-bold'>Items</h2>
            <DataTable 
                data={product?.productItems}   
                keys={Object.keys(product?.productItems[0])} 
                keyfilter={Object.keys(product?.productItems[0])} 
                options={false}
            />
            </>
        :null
    }


        
</div>
  )
}

export default ProductDetails
