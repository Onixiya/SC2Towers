﻿namespace SC2Expansion.Towers{
    public class Hydralisk:ModTower<ZergSet>{
        public static AssetBundle TowerAssets=AssetBundle.LoadFromMemory(Assets.Assets.hydralisk);
        public override string BaseTower=>"DartMonkey";
        public override int Cost=>400;
        public override int TopPathUpgrades=>4;
        public override int MiddlePathUpgrades=>4;
        public override int BottomPathUpgrades=>0;
        public override bool DontAddToShop=>!ZergEnabled;
        public override string Description=>"Ranged Zerg shock trooper. Shoots spines";
        public override void ModifyBaseTowerModel(TowerModel Hydralisk){
            Hydralisk.display="HydraliskPrefab";
            Hydralisk.portrait=new("HydraliskPortrait");
            Hydralisk.icon=new("HydraliskIcon");
            Hydralisk.radius=7;
            Hydralisk.range=30;
            var Spines=Hydralisk.GetAttackModel();
            Spines.weapons[0].rate=0.7f;
            Spines.range=Hydralisk.range;
            Spines.weapons[0].projectile.GetDamageModel().damage=2;
            Hydralisk.GetBehavior<DisplayModel>().display=Hydralisk.display;
            Hydralisk.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1.assetId="HydraliskBirth";
            Hydralisk.GetBehavior<CreateSoundOnTowerPlaceModel>().sound2=Hydralisk.GetBehavior<CreateSoundOnTowerPlaceModel>().sound1;
            SetUpgradeSounds(Hydralisk,"HydraliskUpgrade");
        }
        public class GroovedSpines:ModUpgrade<Hydralisk>{
            public override string Name=>"GroovedSpines";
            public override string DisplayName=>"Grooved Spines";
            public override string Description=>"Evolving small grooves into the spines increases their range";
            public override int Cost=>750;
            public override int Path=>TOP;
            public override int Tier=>1;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskGroovedSpinesIcon");
                Hydralisk.range+=10;
                Hydralisk.GetAttackModel().range=Hydralisk.range;
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade1");
            }
        }
        public class MuscularAugments:ModUpgrade<Hydralisk>{
            public override string Name=>"MuscularAugments";
            public override string DisplayName=>"Muscular Augments";
            public override string Description=>"More muscles are now used to throw spines increasing damage";
            public override int Cost=>750;
            public override int Path=>MIDDLE;
            public override int Tier=>1;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskMuscularAugmentsIcon");
                Hydralisk.GetAttackModel().weapons[0].projectile.GetDamageModel().damage+=2;
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade2");
            }
        }
        public class Frenzy:ModUpgrade<Hydralisk>{
            public override string Name=>"Frenzy";
            public override string DisplayName=>"Frenzy";
            public override string Description=>"By injecting testosterone, massive increase in agression can be done increasing attack speed for a short time";
            public override int Cost=>750;
            public override int Path=>TOP;
            public override int Tier=>2;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskFrenzyIcon");
                var Frenzy=Game.instance.model.GetTowerFromId("BoomerangMonkey-040").GetBehavior<AbilityModel>();
                var FrenzyEffect=Frenzy.GetBehavior<TurboModel>();
                Frenzy.name="Frenzy";
                Frenzy.displayName="Frenzy";
                Frenzy.icon=new("HydraliskFrenzyIcon");
                Frenzy.cooldown=60;
                Frenzy.maxActivationsPerRound=1;
                FrenzyEffect.projectileDisplay=null;
                FrenzyEffect.lifespan=15;
                Hydralisk.AddBehavior(Frenzy);
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade3");
            }
        }
        //i know this belongs to the lurker but i haven't got a clue on where else to put it, lurker needs to be t3 to avoid any model issues and impalers are the t4
        public class SeismicSpines:ModUpgrade<Hydralisk>{
            public override string Name=>"SeismicSpines";
            public override string DisplayName=>"Seismic Spines";
            public override string Description=>"Even more muscles are used to throw spines, enough to break through Lead Bloons";
            public override int Cost=>750;
            public override int Path=>MIDDLE;
            public override int Tier=>2;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskSeismicSpinesIcon");
                var Spines=Hydralisk.GetAttackModel();
                Spines.weapons[0].projectile.GetDamageModel().damage+=2;
                Spines.weapons[0].projectile.GetDamageModel().immuneBloonProperties=0;
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade4");
            }
        }
        public class Primal:ModUpgrade<Hydralisk>{
            public override string Name=>"HydraliskPrimal";
            public override string DisplayName=>"Primal Evolution";
            public override string Description=>"Gains a random bonus to attack speed, damage or range";
            public override int Cost=>750;
            public override int Path=>TOP;
            public override int Tier=>3;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskPrimalIcon");
                Hydralisk.display="HydraliskPrimalPrefab";
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade5");
            }
        }
        public class Lurker:ModUpgrade<Hydralisk>{
            public override string Name=>"Lurker";
            public override string DisplayName=>"Evolve into Lurker";
            public override string Description=>"Evolves into a Lurker changing its attack into a long range attack that damages everything in its path";
            public override int Cost=>750;
            public override int Path=>MIDDLE;
            public override int Tier=>3;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskLurkerIcon");
                Hydralisk.display="HydraliskLurkerPrefab";
                Hydralisk.portrait=new("HydraliskLurkerPortrait");
                Hydralisk.radius=10;
                Hydralisk.range+=20;
                Hydralisk.RemoveBehavior<AttackModel>();
                Hydralisk.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-030").behaviors.First(a=>a.name.Contains("Dragon")).Cast<AttackModel>().Duplicate());
                var Spines=Hydralisk.GetAttackModel();
                Spines.range=Hydralisk.range;
                Spines.weapons[0].projectile.display="HydraliskLurkerSpinesPrefab";
                Spines.weapons[0].projectile.pierce=9999999;
                Spines.weapons[0].projectile.GetDamageModel().damage=1;
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade6");
            }
        }
        public class HunterKiller:ModUpgrade<Hydralisk>{
            public override string Name=>"HunterKiller";
            public override string DisplayName=>"Hunter Killer";
            public override string Description=>"Hunter Killers are a elite strain of Hydralisks, extremely aggresive and lethal";
            public override int Cost=>750;
            public override int Path=>TOP;
            public override int Tier=>4;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskHunterKillerIcon");
                Hydralisk.display="HydraliskHunterKillerPrefab";
                Hydralisk.portrait=new("HydraliskHunterKillerPortrait");
                Hydralisk.range+=5;
                var Spines=Hydralisk.GetAttackModel();
                Spines.weapons[0].projectile.GetDamageModel().damage+=2;
                Spines.range=Hydralisk.range;
                Spines.weapons[0].rate-=0.3f;
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade7");
            }
        }
        public class Impaler:ModUpgrade<Hydralisk>{
            public override string Name=>"Impaler";
            public override string DisplayName=>"Evolve into Impaler";
            public override string Description=>"Impalers are a mobile strain of the sunken colony dealing massive damage to single targets";
            public override int Cost=>750;
            public override int Path=>MIDDLE;
            public override int Tier=>4;
            public override void ApplyUpgrade(TowerModel Hydralisk){
                GetUpgradeModel().icon=new("HydraliskImpalerIcon");
                Hydralisk.display="HydraliskImpalerPrefab";
                Hydralisk.portrait=new("HydraliskImpalerPortrait");
                Hydralisk.RemoveBehaviors<AttackModel>();
                var AttToAdd=Game.instance.model.towers.First(a=>a.name.Contains("SniperMonkey-500")).Cast<TowerModel>().behaviors.
                    First(a=>a.name.Contains("Attack")).Clone().Cast<AttackModel>();
                Hydralisk.range+=25;
                AttToAdd.range=Hydralisk.range;
                AttToAdd.weapons[0].projectile.display="HydraliskImpalerAttackPrefab";
                AttToAdd.weapons[0].projectile.AddBehavior(Game.instance.model.GetTowerFromId("WizardMonkey-030").behaviors.First(a=>a.name.Contains("Fireball")).Cast<AttackModel>().
                    weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>());
                AttToAdd.weapons[0].projectile.GetBehavior<CreateEffectOnContactModel>().effectModel.assetId="HydraliskImpalerAttackPrefab";
                SetUpgradeSounds(Hydralisk,"HydraliskUpgrade8");
            }
        }
        [HarmonyPatch(typeof(AudioFactory),"Start")]
        public class AudioFactoryStart_Patch{
            [HarmonyPostfix]
            public static void Prefix(ref AudioFactory __instance){
                if(TerranEnabled){
                    AudioFactoryInstance=__instance;
                    __instance.RegisterAudioClip("HydraliskBirth",TowerAssets.LoadAsset("HydraliskBirth").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade",TowerAssets.LoadAsset("HydraliskUpgrade").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade1",TowerAssets.LoadAsset("HydraliskUpgrade1").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade2",TowerAssets.LoadAsset("HydraliskUpgrade2").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade3",TowerAssets.LoadAsset("HydraliskUpgrade3").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade4",TowerAssets.LoadAsset("HydraliskUpgrade4").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade5",TowerAssets.LoadAsset("HydraliskUpgrade5").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade6",TowerAssets.LoadAsset("HydraliskUpgrade6").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade7",TowerAssets.LoadAsset("HydraliskUpgrade7").Cast<AudioClip>());
                    __instance.RegisterAudioClip("HydraliskUpgrade8",TowerAssets.LoadAsset("HydraliskUpgrade8").Cast<AudioClip>());
                }
            }
        }
        [HarmonyPatch(typeof(TowerManager),"UpgradeTower")]
        public static class TowerManagerUpgradeTower_Patch{
            [HarmonyPostfix]
            public static void Postfix(Tower tower,ref TowerModel def,string __state){
                if(__state!=null&&__state.Contains("Primal")&&tower.namedMonkeyKey.Contains("Hydralisk")){
                    int RandNum=new System.Random().Next(1,3);
                    if(RandNum==1){
                        def.GetAttackModel().range+=5;
                        def.range=def.GetAttackModel().range;
                    }
                    if(RandNum==2)def.GetAttackModel().weapons[0].rate-=0.2f;
                    if(RandNum==3)def.GetAttackModel().weapons[0].projectile.GetDamageModel().damage+=2;
                }
            }
        }
        [HarmonyPatch(typeof(Factory),"FindAndSetupPrototypeAsync")]
        public class FactoryFindAndSetupPrototypeAsync_Patch{
            [HarmonyPrefix]
            public static bool Prefix(Factory __instance,string objectId,Il2CppSystem.Action<UnityDisplayNode>onComplete){
                if(!DisplayDict.ContainsKey(objectId)&&objectId.Contains("Hydralisk")){
                    var udn=uObject.Instantiate(TowerAssets.LoadAsset(objectId).Cast<GameObject>(),__instance.PrototypeRoot).AddComponent<UnityDisplayNode>();
                    udn.name="SC2Expansion-Hydralisk";
                    udn.isSprite=false;
                    onComplete.Invoke(udn);
                    DisplayDict.Add(objectId,udn);
                    return false;
                }
                if(DisplayDict.ContainsKey(objectId)){
                    onComplete.Invoke(DisplayDict[objectId]);
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(ResourceLoader),"LoadSpriteFromSpriteReferenceAsync")]
        public record ResourceLoaderLoadSpriteFromSpriteReferenceAsync_Patch{
            [HarmonyPostfix]
            public static void Postfix(SpriteReference reference,ref uImage image){
                if(reference!=null&&reference.guidRef.Contains("Hydralisk")){
                    var text=TowerAssets.LoadAsset(reference.guidRef).Cast<Texture2D>();
                    image.canvasRenderer.SetTexture(text);
                    image.sprite=Sprite.Create(text,new(0,0,text.width,text.height),new());
                }
            }
        }
        [HarmonyPatch(typeof(Weapon),"SpawnDart")]
        public static class WeaponSpawnDart_Patch{
            [HarmonyPostfix]
            public static void Postfix(ref Weapon __instance){
                if(__instance.attack.tower.towerModel.name.Contains("Hydralisk")){
                    __instance.attack.tower.Node.graphic.GetComponentInParent<Animator>().Play("HydraliskAttack");
                }
            }
        }
    }
}