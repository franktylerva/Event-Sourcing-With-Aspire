import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import Keycloak from 'keycloak-js';

const keycloak = new Keycloak({
    url: "http://localhost:8080",
    realm: "event-sourcing",
    clientId: "react-client"
});

try {

    const authenticated = await keycloak.init({
        onLoad: 'login-required'
    });

    if (authenticated) {
        console.log('User is authenticated');
    } else {
        console.log('User is not authenticated');
    }
} catch (error) {
    console.error('Failed to initialize adapter:', error);
}

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.jsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}
export default App
