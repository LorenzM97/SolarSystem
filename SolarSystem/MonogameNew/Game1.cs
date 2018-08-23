using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.ObjectModel;
using Library_Solarsystem;
using System.IO;
using Newtonsoft.Json;

namespace Monogame
{
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Communication c = new Communication();
        Texture2D background, sun, planet, moon;
        Solarsystem s = new Solarsystem();
        public float geschwindigkeit = 1;

        ObservableCollection<SpaceObject> _listTmp = new ObservableCollection<SpaceObject>();
        ObservableCollection<SpaceObject> _listSolarSystem = new ObservableCollection<SpaceObject>();
        public Game1()
        {
            WidthHeight.screenWidth = 1350;
            WidthHeight.screenHight = 700;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WidthHeight.screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = WidthHeight.screenHight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Initialize();
        }

      
        protected override void Initialize()
        {

            string jsonText = File.ReadAllText("../../../../../jsonSolarsystems.txt");
            _listTmp = JsonConvert.DeserializeObject<ObservableCollection<SpaceObject>>(jsonText);
            string index = File.ReadAllText("../../../../../Index.txt");
            var item = _listTmp[int.Parse(index)];
            {
                foreach (var planet in item.ListPlanets)

                    if (planet.Type == "planet")
                    {
                        SpaceObject tempPlanet = new SpaceObject(planet.Type, planet.Name, planet.Size, planet.Distance, planet.Degree);
                        _listSolarSystem.Add(tempPlanet);

                        foreach(var moon in planet.ListMoons)
                        {
                            SpaceObject moonTemp = new SpaceObject(moon.Type, moon.Name, tempPlanet, moon.Size, moon.Distance, moon.Degree);
                            _listSolarSystem.Add(moonTemp);
                            tempPlanet.ListMoons.Add(moonTemp);
                        }
                    } else if(planet.Type == "sun")
                    {
                        _listSolarSystem.Add(new SpaceObject(WidthHeight.screenWidth/2, WidthHeight.screenHight/2, planet.Type, planet.Name, planet.Size));

                    }
            }

            base.Initialize();
        }

   
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Bilder/Galaxie");
            sun = Content.Load<Texture2D>("Bilder/Sonne");
            planet = Content.Load<Texture2D>("Bilder/Merkur");
            moon = Content.Load<Texture2D>("Bilder/mond");
        }


        protected override void UnloadContent()
        {

        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
            foreach(var item in _listSolarSystem)
            {
                if(item.Type == "planet")
                {
                    item.Move(geschwindigkeit, WidthHeight.screenWidth / 2, WidthHeight.screenHight / 2);

                }else if (item.Type == "moon")
                {
                    item.Move(geschwindigkeit);
                }
            }

            


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height), Color.White);

            foreach (var item in _listSolarSystem)
            {
                if (item.Type == "planet")
                {
                    spriteBatch.Draw(planet, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(planet.Width / 2, planet.Height / 2), (float)(1f / item.Size), SpriteEffects.None, 0);

                }
                else
                    if (item.Type == "sun")
                {
                    spriteBatch.Draw(sun, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(sun.Width / 2, sun.Height / 2), (float)(1f / item.Size), SpriteEffects.None, 0);

                }
                else if (item.Type == "moon")
                {
                    spriteBatch.Draw(moon, new Vector2(item.X, item.Y), null, Color.White, 0, new Vector2(moon.Width / 2, moon.Height / 2), (float) (1f/item.Size) , SpriteEffects.None, 0);
                }


            }




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
