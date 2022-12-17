using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiScrapingData.Core.Repositories;
using WebApiScrapingData.Core.Repositories.RepositoriesQuizz;
using WebApiScrapingData.Domain.Class;
using WepApiScrapingData.ExtensionMethods;

namespace WepApiScrapingData.Controllers
{
    
    [ApiController]
    [Route("api/v1.0/[controller]")]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class TypeAttaqueController : ControllerBase
    {
        #region Fields
        private readonly IRepository<TypeAttaque> _repository;
        private readonly IRepository<TypePok> _repositoryTP;
        private readonly IRepository<Attaque> _repositoryA;
        #endregion

        #region Constructors
        public TypeAttaqueController(IRepository<TypeAttaque> repository, IRepository<TypePok> repositoryTP, IRepository<Attaque> repositoryA)
        {
            _repository = repository;
            _repositoryTP = repositoryTP;
            _repositoryA = repositoryA;
        }
        #endregion

        [HttpGet]
        public async Task<IEnumerable<TypeAttaque>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<TypeAttaque> GetSingle(int id)
        {
            return await _repository.Get(id);
        }

        [HttpGet]
        [Route("FindByName/{name}")]
        public IEnumerable<TypeAttaque> GetFindByName(string name)
        {
            return _repository.Find(m => m.Name_FR.Equals(name));
        }

        [HttpPut]
        [Route("UpdateTypeAttaqueInDB/{id}")]
        public async Task UpdateTypeAttaqueInDB()
        {
            List<TypeAttaque> typeAttaques = new List<TypeAttaque>();

            TypeAttaque typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = "Capacités Physiques";
            typeAttaque.Description_FR = "Les capacités physiques utilisent les statistiques physiques (attaque et défense)";
            typeAttaque.Name_EN = "Physical Abilities";
            typeAttaque.Description_EN = "Physical abilities use physical stats (attack & defense)";
            typeAttaque.Name_ES = "Habilidades Fisicas";
            typeAttaque.Description_ES = "Las habilidades físicas usan estadísticas físicas (ataque y defensa)";
            typeAttaque.Name_IT = "Abilità Fisiche";
            typeAttaque.Description_IT = "Le abilità fisiche usano le statistiche fisiche (attacco e difesa)";
            typeAttaque.Name_DE = "Körperliche Fähigkeiten";
            typeAttaque.Description_DE = "Physische Fähigkeiten verwenden physische Werte (angriff & verteidigung)";
            typeAttaque.Name_RU = "Физические способности";
            typeAttaque.Description_RU = "Физические способности используют физические характеристики (атака и защита)";
            typeAttaque.Name_CO = "신체적 능력";
            typeAttaque.Description_CO = "신체 능력은 신체 능력치(공격 및 방어)를 사용합니다";
            typeAttaque.Name_CN = "体能";
            typeAttaque.Description_CN = "物理能力使用物理统计（攻击和防御）";
            typeAttaque.Name_JP = "身体能力";
            typeAttaque.Description_JP = "物理アビリティは物理ステータスを使用します (攻撃と防御)";
            typeAttaque.UrlImg = "https://www.pokepedia.fr/images/1/11/Miniature_Cat%C3%A9gorie_Physique_DEPS.png";
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = "Capacités Spéciales";
            typeAttaque.Description_FR = "Les capacités spéciales utilisent les statistiques spéciales (attaque spéciale et défense spéciale, ou uniquement le spécial dans la première génération)";
            typeAttaque.Name_EN = "Special Abilities";
            typeAttaque.Description_EN = "Special abilities use special stats (special attack and special defense, or only the special in the first generation)";
            typeAttaque.Name_ES = "Habilidades Especiales";
            typeAttaque.Description_ES = "Las habilidades especiales usan estadísticas especiales (ataque especial y defensa especial, o solo las especiales en la primera generación)";
            typeAttaque.Name_IT = "Abilità Speciali";
            typeAttaque.Description_IT = "Le abilità speciali usano statistiche speciali (attacco speciale e difesa speciale, o solo lo speciale nella prima generazione)";
            typeAttaque.Name_DE = "Spezielle Fähigkeiten";
            typeAttaque.Description_DE = "Spezialfähigkeiten verwenden Spezialstatistiken (Spezialangriff und Spezialverteidigung oder nur das Spezial in der ersten Generation)";
            typeAttaque.Name_RU = "Специальные возможности";
            typeAttaque.Description_RU = "Специальные способности используют специальные характеристики (специальная атака и специальная защита или только специальные в первом поколении)";
            typeAttaque.Name_CO = "특수 능력";
            typeAttaque.Description_CO = "특수 능력은 특수 능력치를 사용합니다(특수 공격과 특수 방어, 또는 1세대에서 특수만 사용)";
            typeAttaque.Name_CN = "特殊的能力";
            typeAttaque.Description_CN = "特殊能力使用特殊属性（特攻特防，或者只有初代特攻）";
            typeAttaque.Name_JP = "特殊能力";
            typeAttaque.Description_JP = "特殊能力は特殊な統計を使用します（特殊攻撃と特殊防御、または第一世代の特殊のみ）";
            typeAttaque.UrlImg = "https://www.pokepedia.fr/images/d/da/Miniature_Cat%C3%A9gorie_Sp%C3%A9ciale_DEPS.png";
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            typeAttaque = new TypeAttaque();
            typeAttaque.Name_FR = "Capacités de Statut";
            typeAttaque.Description_FR = "Les capacités de statut sont toutes les autres capacités, celles qui ne provoquent pas de dégâts selon la formule habituelle (dégâts fixes, augmentation de statistiques, changement de statut, etc.)";
            typeAttaque.Name_EN = "Status Abilities";
            typeAttaque.Description_EN = "Status abilities are all other abilities, those that don't deal damage in the usual formula (fixed damage, stat increase, status change, etc.)";
            typeAttaque.Name_ES = "Habilidades de Estado";
            typeAttaque.Description_ES = "Las habilidades de estado son todas las demás habilidades, aquellas que no infligen daño en la fórmula habitual (daño fijo, aumento de estadísticas, cambio de estado, etc.)";
            typeAttaque.Name_IT = "Abilità di Stato";
            typeAttaque.Description_IT = "Le abilità di stato sono tutte le altre abilità, quelle che non infliggono danno nella solita formula (danno fisso, aumento delle statistiche, cambio di stato, ecc.)";
            typeAttaque.Name_DE = "Statusfähigkeiten";
            typeAttaque.Description_DE = "Statusfähigkeiten sind alle anderen Fähigkeiten, die keinen Schaden in der üblichen Formel (fester Schaden, Statuserhöhung, Statusänderung usw.)";
            typeAttaque.Name_RU = "Способности статуса";
            typeAttaque.Description_RU = "Статусные способности — это все остальные способности, которые не наносят урон в обычной формуле (фиксированный урон, повышение характеристик, изменение статуса и т.д.)";
            typeAttaque.Name_CO = "상태 능력";
            typeAttaque.Description_CO = "스테이터스 능력은 일반적인 공식(데미지 고정, 스탯 증가, 스테이터스 변경 등)으로 피해를 입히지 않는 다른 모든 능력입니다.";
            typeAttaque.Name_CN = "状态能力";
            typeAttaque.Description_CN = "状态能力是所有其他能力，那些在通常的公式中不造成伤害的能力（固定伤害、属性增加、状态改变等）";
            typeAttaque.Name_JP = "ステータスアビリティ";
            typeAttaque.Description_JP = "ステータスアビリティは、通常の公式ではダメージを与えないアビリティ（固定ダメージ、ステータス上昇、ステータス変化など）です。";
            typeAttaque.UrlImg = "https://www.pokepedia.fr/images/b/b5/Miniature_Cat%C3%A9gorie_Statut_DEPS.png";
            if (_repository.Find(m => m.Name_FR.Equals(typeAttaque.Name_FR)).Count() == 0)
                typeAttaques.Add(typeAttaque);

            await _repository.AddRange(typeAttaques);
            _repository.UnitOfWork.SaveChanges();
        }
    }
}
