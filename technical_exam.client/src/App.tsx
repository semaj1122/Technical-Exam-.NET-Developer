import { useEffect, useState } from 'react';
import './App.css';

interface CartList {
    id: string;
    productName: number;
    cost: number;
    quantity: number;
    amount: number;
}

interface RemoveCartItem {
    userId: string,
    cartId: string
}

function App() {
    const [cartList, setCartList] = useState<CartList[]>();

    useEffect(() => {
        populateCart();
    }, []);

    const onClickDelete = (cartId:any ) => {
        removeCart({userId: '1', cartId: cartId})
    }

    const onClickSearch = () => {

    }

    const onClickSave = () => {

    }

    const contents = cartList === undefined
        ? ''
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Product ID</th>
                    <th>Product Name</th>
                    <th>Cost</th>
                    <th>Quantity</th>
                    <th>Amount</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                {cartList.map(x =>
                    <tr key={x.id}>
                        <td>{x.id}</td>
                        <td>{x.productName}</td>
                        <td>{x.cost}</td>
                        <td>{x.quantity}</td>
                        <td>{x.amount}</td>
                        <td><button onClick={ () => onClickDelete(x.id) }>delete</button></td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <table style={{textAlign: 'left'}} cellPadding={5}>
                <tr>
                    <td colspan="2">
                        <label>Search Product:</label>
                        <input type='text' />
                        <button onClick={onClickSearch}>search</button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Product Id:</label>
                        <input type='text' />
                    </td>
                    <td>
                        <label>Product Name:</label>
                        <input type='text' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Cost:</label>
                        <input type='text' />
                    </td>
                    <td>
                        <label>Quantity:</label>
                        <input type='text' />
                    </td>
                </tr>
            </table>
            {contents}

            <div style={{marginTop: 20, width: '100%', textAlign: 'right'}}>
                <div style={{marginBottom: 10}}>
                    <label>Total Amount: </label>
                    <input type='text' />
                </div>
                <div style={{marginBottom: 10}}>
                    <label>Cash: </label>
                    <input type='text' />
                </div>
                <div style={{marginBottom: 10}}>
                    <label>Change: </label>
                    <input type='text' />
                </div>
                <div style={{marginBottom: 10}}>
                    <button onClick={() => onClickSave()}>Save</button>
                </div>
            </div>
        </div>
    );

    async function populateCart() {
        const url = 'https://localhost:7243/product/get-cart-list?userId=1';
        const requestOptions: RequestInit = {
          method: 'GET',
          headers: {
            'accept': 'text/plain'
          },
        };

        const response = await fetch(url, requestOptions);

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // Assuming the response is in text format
        const responseData = await response.text();
        const jsonData =JSON.parse(responseData);
        console.log(jsonData.data)
        setCartList(jsonData.data);
    }

    async function removeCart(obj: RemoveCartItem){
        const requestOptions = {
            method: 'POST', 
            headers: { 'Content-Type': 'application/json'},
            body: JSON.stringify(obj) 
        };
        fetch('remove-cart', requestOptions)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json(); 
        })
        .then(data => {
            if(data.statusCode == '00'){
                populateCart()
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
    }
}

export default App;