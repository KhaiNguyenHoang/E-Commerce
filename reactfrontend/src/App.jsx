import './App.css';
import { Routes, Route } from 'react-router-dom';
import ProductPage from './domain/product/pages/ProductPage.jsx';
import ProductAdd from './domain/product/components/ProductAdd.jsx';

function App() {

  return (
    <Routes>
      <Route path="/" element={<ProductPage />} />
      <Route path="/add" element={<ProductAdd />} />
    </Routes>
  )
}

export default App
