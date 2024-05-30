int opcion = 0;
do
{
    try
    {
        Console.WriteLine("Bienvenido A Battle Ship");
        Console.WriteLine("1) Iniciar");
        Console.WriteLine("2) Salir");
        opcion = Convert.ToInt32(Console.ReadLine());
        switch (opcion)
        {
            case 1:
                int[,] mapa = new int[20, 20];
                int[] tamanos = { 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
                string[] nombres = { "Portaaviones", "Destructor", "Destructor", "Destructor", "Fragata", "Fragata", "Fragata", "Submarino", "Submarino", "Submarino", "Submarino" };
                JuegoDeGuerra(mapa, tamanos, nombres);
                break;
            case 2:
                Console.WriteLine("Gracias por jugar.");
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }
    catch (Exception)
    {
        Console.Clear();
        Console.WriteLine("Entrada no valida.\n");
    }

} while (opcion != 2);

static void JuegoDeGuerra(int[,] mapa, int[] tamanos, string[] nombres)
{
    ColocarNaves(mapa, tamanos, nombres);
    int contadorDisparos = 0;
    int aciertos = 20;
    int puntos = 10000;
    do
    {
        Console.WriteLine("Debes acertar un total de {0} disparos", aciertos);
        Console.WriteLine("Cañonazos: " + contadorDisparos);
        Console.WriteLine("Puntaje: " + puntos);
        Console.WriteLine();
        MostrarMapa(mapa);
        Console.WriteLine();
        try
        {
            Console.WriteLine("\nIngrese una Fila,Columna: ");
            Console.Write("- ");
            string[] posicion = Console.ReadLine().Split(',');
            int fila = int.Parse(posicion[0]) - 1;
            int columna = int.Parse(posicion[1]) - 1;

            if (mapa[fila, columna] == 7)
            {
                Console.Clear();
                Console.WriteLine("¡Fallaste! -100pts\n");
                puntos -= 100;
            }
            else if (mapa[fila, columna] == 5)
            {
                Console.Clear();
                Console.WriteLine("¡Fallaste! -100pts\n");
                puntos -= 100;
            }
            else if (mapa[fila, columna] > 0 && mapa[fila, columna] < 8)
            {
                mapa[fila, columna] = 7; 
                Console.Clear();
                Console.WriteLine("¡Acertaste! +200pts\n");
                puntos += 200;
                aciertos--;
            }
            else if (mapa[fila, columna] == 0)
            {
                mapa[fila, columna] = 5; 
                Console.Clear();
                Console.WriteLine("¡Fallaste! -100pts\n");
                puntos -= 100; 
            }
            contadorDisparos++;
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Ubicación no válida");
            Console.ReadKey();
        }
    } while (aciertos > 0 && puntos > 0);

    if (puntos <= 0)
    {
        Console.WriteLine("Game Over");
    }
    else
    {
        Console.WriteLine("¡Victoria!");
    }

    Console.WriteLine("Presiona ENTER");
    Console.ReadKey();
}

static void MostrarMapa(int[,] mapa)
{
    Console.Write("   ");
    for (int col = 1; col <= mapa.GetLength(1); col++)
    {
        Console.Write($"{col,3}");
    }
    Console.WriteLine();

    for (int fila = 1; fila <= mapa.GetLength(0); fila++)
    {
        Console.Write($"{fila,3}");
        for (int col = 0; col < mapa.GetLength(1); col++)
        {
            int valorCelda = mapa[fila - 1, col];
            char simbolo;
            if (valorCelda == 7)
            {
                simbolo = 'V';
            }
            else if (valorCelda == 5) 
            {
                simbolo = 'F';
            }
            else 
            {
                simbolo = '0';
            }
            Console.Write($"{simbolo,3}");
        }
        Console.WriteLine();
    }
}


static void ColocarNaves(int[,] mapa, int[] tamanos, string[] nombres)
{
    Random aleatorio = new Random();
    for (int i = 0; i < tamanos.Length; i++)
    {
        for (int j = 0; j < 1; j++) 
        {
            bool posicionValida;
            do
            {
                posicionValida = true;
                int filaInicial = aleatorio.Next(mapa.GetLength(0));
                int columnaInicial = aleatorio.Next(mapa.GetLength(1));
                int direccion = aleatorio.Next(2); 

                if ((direccion == 0 && columnaInicial + tamanos[i] > mapa.GetLength(1)) ||
                    (direccion == 1 && filaInicial + tamanos[i] > mapa.GetLength(0)))
                {
                    posicionValida = false;
                }
                else
                {
                    for (int k = 0; k < tamanos[i]; k++)
                    {
                        if (direccion == 0 && mapa[filaInicial, columnaInicial + k] > 0 ||
                            direccion == 1 && mapa[filaInicial + k, columnaInicial] > 0)
                        {
                            posicionValida = false;
                            break;
                        }
                    }
                }

                if (posicionValida)
                {
                    for (int k = 0; k < tamanos[i]; k++)
                    {
                        if (direccion == 0)
                        {
                            mapa[filaInicial, columnaInicial + k] = i + 1;
                        }
                        else
                        {
                            mapa[filaInicial + k, columnaInicial] = i + 1;
                        }
                    }
                }
            } while (!posicionValida);
        }
    }
}

