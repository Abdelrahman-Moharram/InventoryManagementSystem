import React, { useEffect, useState } from 'react'
import DataTable from '../../Components/Tables/DataTable'
import { getAllProducts } from '../../Api/Products'

const Products = () => {
  const [products, setProducts] = useState([])
  useEffect(()=>{
    getAllProducts().then(data=>{
      setProducts(data.data)
    }).catch(err=>{
      console.log(err);
    })
  },[])
  let keyfilter = []
  return (
    <div className='w-full'>
      {
        products[0]?
        <DataTable data={products} keys={Object.keys(products[0]).filter(
          el=>!el
          .toLowerCase().includes("id") && !el.includes("description"))

          .map(el=>{
            keyfilter.push(el)
            return el.replace("Name", "")
          })
          .map(el=>el.replace("products",""))
          .map(el=>el.replace("product",""))
          .map(el=>el.replace("product",""))
        } 

        keyfilter={keyfilter}
        
        />
        :
        null
      }
    </div>
  )
}

export default Products
