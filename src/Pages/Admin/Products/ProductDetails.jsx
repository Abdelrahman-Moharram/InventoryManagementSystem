import React, { useEffect, useState } from 'react'
import ImageSekelton from '../../../Components/Sekeltons/ImageSekelton'
import { getProductById } from '../../../Api/Products'
import { useParams } from 'react-router-dom'
import ProductInfo from '../../../Components/Lists/ProductsInfo'
import DataTable from '../../../Components/Tables/DataTable'
import config from 'dotenv'
const server = `${process.env.SERVER_URL}api/accounts/`


const ProductDetails = () => {
    const [product, setProduct] = useState({})
    const params = useParams()
    useEffect(()=>{
      getProductById(params.id).then(res=>{
        setProduct(res.data)
      })
    },[params.productId])
    const keyfilter=[]
    return (
<div className='container-fluid px-4 w-full'>
 
        {/* <div className='px-10 md:px-28 py-8'>
          <BreadCrumb  sections={path.split("/").slice(2)} />
        </div> */}
      
    <div className="grid grid-cols-1 sm:grid-cols-2 mt-10 gap-5 place-items-center mb-5">
        {/* image */}
        <div className='default-shadow rounded-lg overflow-hidden'>
            {
            product?.images?
                <img 
                    src={process.env.SERVER_URL + product?.images[0]}

                    alt='banner'
                    sizes="100vw"
                    style={{ height: 'auto' }}
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
    

    {/* 
        ------------------------------------------------------------------
        Items 
        ------------------------------------------------------------------
    */}
    {
        
        product?.productItems?.length
        ?
        
            <>
            <div className='default-shadow rounded-lg p-10 my-4'>
                <h2 className='text-xl font-bold text-center'>Items</h2>

                <DataTable 
                    data={product?.productItems}   
                    keyfilter={keyfilter} 
                    options={false}
                    keys={Object.keys(product?.productItems[0]).filter(
                        el=>!el
                        .toLowerCase().includes("id") && !el.includes("description"))
                        .map(el=>{
                            keyfilter.push(el)
                            return el.replace("Name", "")
                        })
                    } 
                        
                />
            </div>
            </>
        :null
    }
    
    {/* keys={Object.keys(products[0]).filter(
          el=>!el
          .toLowerCase().includes("id") && !el.includes("description"))

          .map(el=>{
            keyfilter.push(el)
            return el.replace("Name", "")
          })
          .map(el=>el.replace("products",""))
          .map(el=>el.replace("product",""))
          .map(el=>el.replace("product",""))
        }  */}


        
</div>
  )
}

export default ProductDetails
