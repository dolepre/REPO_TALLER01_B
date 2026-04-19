/*
 * Creado por SharpDevelop.
 * Usuario: Daniel Mujica
 * Fecha: 17/4/2026
 */
using System;
using System.IO;

namespace Persitencia01
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("TALLER SECCION B");
			
			string rutaRaiz = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatosIujo");
			string rutaReportes = Path.Combine(rutaRaiz, "REPORTES");
			
			if(!Directory.Exists(rutaReportes)){
				Directory.CreateDirectory(rutaReportes);
				Console.WriteLine("Directorio Creado Correctamente");
			}

			//Desafío 1: validador de seguridad:
			
			// Primer paso: Recibir la cadena
			string entradaUsuario = "admin;clave123";
			
			// Segundo paso: .Split(';')
			string[] partes = entradaUsuario.Split(';');
			string clave = partes[1];
			
			// Tercer piso: Verificar si contiene "123"
			if (clave.Contains("123")) {
				
				string rutaArchivoSeguridad = Path.Combine(rutaReportes, "seguridad.txt");
				
				using (StreamWriter sw = new StreamWriter(rutaArchivoSeguridad, true)) {
					sw.WriteLine("Clave débil detectada");
				}
				Console.WriteLine("--- Desafío 1: Alerta de seguridad guardada.");
			}

			//Desafío 2: clonar imagenes:
			string origenImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "avatar.jpg");
			string destinoImg = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "respaldo.jpg");

			if (File.Exists(origenImg)) {
				using (FileStream fsOrigen = new FileStream(origenImg, FileMode.Open, FileAccess.Read))
				using (FileStream fsDestino = new FileStream(destinoImg, FileMode.Create, FileAccess.Write)) {
					
					byte[] buffer = new byte[1024]; // el buffer de 1KB que pide el PDF
					int bytesLeidos;
					
					// ciclo que permite leer hasta que no queden bytes
					while ((bytesLeidos = fsOrigen.Read(buffer, 0, buffer.Length)) > 0) {
						fsDestino.Write(buffer, 0, bytesLeidos);
					}
				}
				Console.WriteLine("--- Desafío 2: Imagen copiada exitosamente.");
			} else {
				Console.WriteLine("--- Desafío 2: No se encontró avatar.jpg.");
			}

			// Desafío 3: buscar archivos pesados:
			Console.WriteLine("--- Desafío 3: Buscando archivos pesados...");
			
			// Listar todos los archivos de la carpeta REPORTES
			string[] archivosEncontrados = Directory.GetFiles(rutaReportes);
			
			foreach (string ruta in archivosEncontrados) {
				// Crear el inspector
				FileInfo info = new FileInfo(ruta);
				
				if (info.Length > 5120) {
					Console.WriteLine("Borrando " + info.Name + " por exceder los 5KB.");
					info.Delete(); 
				}
			}

			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}