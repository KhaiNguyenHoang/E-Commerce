import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import './global/tailwindcss.css';
import App from './App.jsx';
import { Provider } from 'react-redux';
import productStore from './domain/product/store/productStore.js';
import { BrowserRouter } from 'react-router-dom';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Provider store={productStore}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </Provider>
  </StrictMode>,
);
