import React, { useEffect, useState } from 'react'
import DataTable from '../../../Components/Tables/DataTable'
import { getAllProducts } from '../../../Api/Products'
import {Link} from 'react-router-dom'
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
    
    <div className='w-full p-5'>
      <Link
        className="inline-block rounded border border-indigo-600 bg-indigo-600 px-12 py-3 text-sm font-medium text-white hover:bg-transparent hover:text-indigo-600 focus:outline-none focus:ring active:text-indigo-500"
        to="add"
      >
        Add New
      </Link>
      <div className='default-shadow rounded-xl p-4 my-4 min-w-full'>
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
    </div>
  )
}

export default Products
