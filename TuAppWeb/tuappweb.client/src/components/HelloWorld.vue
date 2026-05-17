<script setup>
  import { ref, onMounted } from 'vue';

  // Variables reactivas para los datos, el error y el estado de carga
  const datos = ref([]);
  const errorMsg = ref(null);
  const cargando = ref(true);

  // Esta función se ejecuta automáticamente cuando el componente se monta
  onMounted(async () => {
    try {
      // La URL es relativa gracias al proxy configurado
      const response = await fetch('/api/Jokes'); 
      if (!response.ok) {
        throw new Error(`Error HTTP: ${response.status}`);
      }
      datos.value = await response.json();
      console.log(datos.value);
    } catch (error) {
      console.error('Error al obtener datos:', error);
      errorMsg.value = 'No se pudieron cargar los datos. Asegúrate de que el servidor esté corriendo.';
    } finally {
      cargando.value = false;
    }
  });
</script>

<template>
  <div>
    <h1>Mis datos desde SQL Server</h1>

    <!-- Mostrar mensajes de carga o error -->
    <div v-if="cargando" class="alert alert-info">Cargando datos...</div>
    <div v-else-if="errorMsg" class="alert alert-danger">{{ errorMsg }}</div>

    <!-- Mostrar la tabla si todo está bien -->
    <table v-else class="table table-striped">
      <thead>
        <tr>
          <!---->
          <th>Id</th>
          <th>ExternalId</th>
          <th>Content</th>
          <th>CreatedAt</th>
        </tr>
      </thead>
      <tbody>
        <!-- Itera sobre los datos y crea una fila por cada elemento -->
        <tr v-for="item in datos" :key="item.id">
          <!-- 👇 CAMBIA 'item.nombre' por el nombre real de tu columna en SQL -->
          <td>{{ item.id }}</td>
          <td>{{ item.externalId }}</td>
          <td>{{ item.content }}</td>
          <td>{{ item.createdAt }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
  /* Estilos simples y opcionales para la tabla */
  .table {
    width: 100%;
    border-collapse: collapse;
  }

    .table th, .table td {
      border: 1px solid #ddd;
      padding: 8px;
      text-align: left;
    }

    .table th {
      background-color: #f2f2f2;
    }

  .alert {
    padding: 15px;
    margin-bottom: 20px;
    border-radius: 4px;
  }

  .alert-info {
    color: #0c5460;
    background-color: #d1ecf1;
    border-color: #bee5eb;
  }

  .alert-danger {
    color: #721c24;
    background-color: #f8d7da;
    border-color: #f5c6cb;
  }
</style>
