import { useSelector, useDispatch } from 'react-redux';
import { fetchProduct } from '../service/productService.js';
import {useEffect, useState} from "react";
import {Product} from "../entity/Product.js";

function ProductCard() {
    const dispatch = useDispatch();
    const data = useSelector(state => state.product.data);
    const loading = useSelector(state => state.product.loading);
    const error = useSelector(state => state.product.error);

    const [productList, setProductList] = useState([]);

    useEffect(() => {
        dispatch(fetchProduct());
    }, [dispatch]);

    useEffect(() => {
        if (data && data.data) {
            setProductList(data.data);
        }
    }, [data]);

    const content = () => {
        if (loading) {
            return <div className="text-center py-8 text-lg font-medium">Loading products...</div>;
        }

        if (error) {
            return <div className="text-center py-8 text-lg font-medium text-red-600">Error: {error}</div>;
        }

        if (productList.length > 0) {
            return (
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                    {productList.map((product) => {
                        let p =  new Product(product.name, product.image, product.description, product.quantity, product.price, product.discount, product.specialPrice);
                        return (
                            <div key={p.name} className="bg-white rounded-lg shadow-md overflow-hidden transform transition duration-300 hover:scale-105">
                                <img src={p.image} alt={p.name} className="w-full h-48 object-cover" />
                                <div className="p-4">
                                    <h2 className="text-xl font-semibold text-gray-800 mb-2 truncate">{p.name}</h2>
                                    <p className="text-gray-600 text-sm mb-3 line-clamp-2">{p.description}</p>
                                    <div className="flex items-baseline justify-between">
                                        <p className="text-lg font-bold text-indigo-600">${p.price.toFixed(2)}</p>
                                        {p.discount > 0 && (
                                            <span className="text-sm text-red-500 line-through ml-2">${(p.price / (1 - p.discount)).toFixed(2)}</span>
                                        )}
                                    </div>
                                    {p.specialPrice > 0 && (
                                        <p className="text-sm text-green-600 font-medium mt-1">Special Price: ${p.specialPrice.toFixed(2)}</p>
                                    )}
                                    <p className="text-gray-500 text-xs mt-2">Quantity: {p.quantity}</p>
                                </div>
                            </div>
                        );
                    })}
                </div>
            );
        }
        return <div className="text-center py-8 text-lg font-medium text-gray-600">No products found.</div>;
    }

    return (
        <div>
            {content()}
        </div>
    );
}

export default ProductCard;